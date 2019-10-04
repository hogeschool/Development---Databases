module DBGenerator

open System.Text
open System
open Utils
open System.IO

let random = Random() 

exception CodeGenerationException of string

type Value =
| Integer of int
| Text of string
| Real of float
| Boolean of bool
with
  override this.ToString() =
    match this with
    | Integer i -> string i
    | Real r -> string r
    | Text s -> "'" + s + "'"
    | Boolean b -> if b then "true" else "false"

let private getRandomString (length : int) =
  ([1..length] |>
    List.fold(
      fun (text : StringBuilder) _ ->
        text.Append(char (random.Next(97,123)))
    ) (StringBuilder())).ToString()


type SQLType =
| Integer of int * int
| Varchar of int * int
| Text of int
| Date of int * int
| Real of float * float
| Boolean
  with
    member this.Random() : Value =
      match this with
      | Integer (min,max) -> Value.Integer (random.Next(min,max + 1))
      | Real (min,max) -> Value.Real (min + random.NextDouble() * max)
      | Varchar (min,max) ->
          let length = random.Next(min,max + 1)
          Value.Text (getRandomString length)
      | Text length -> 
          Value.Text (getRandomString length)
      | Date (minYear,maxYear)->
          let year = random.Next(minYear,maxYear + 1)
          let month = random.Next(1,13)
          let day =
            match month with
            | 4 | 6 | 9 | 11 -> random.Next(1,31)
            | 2 -> random.Next(1,28)
            | _ -> random.Next(1,32)
          Value.Text (sprintf "%d-%d-%d" year month day)
      | Boolean -> if random.NextDouble() > 0.5 then Value.Boolean true else Value.Boolean false
            

type Column =
  {
    Name        : string
    Type        : SQLType
  }
  with
    static member Create(name,_type) =
      {
        Name = name
        Type = _type
      }

type ForeignKeys = Map<string,List<Column * Column>>

and TableDefinition =
  {
    Name            : string
    Columns         : List<Column>
    ForeignKeys     : ForeignKeys
    PrimaryKey      : List<Column>
    Rows            : int
  }
  with
    static member Create(name,columns,foreignKeys,primaryKey,rows) =
      {
        Name = name
        Columns = columns
        ForeignKeys = foreignKeys
        PrimaryKey = primaryKey
        Rows = rows
      }

type private Record = Map<string,Value>

type private Context =
  {
    Code                  : StringBuilder
    GeneratedTables       : Map<string,List<Record>>
    GeneratedPKs          : Map<string,List<Record>>
  }
  with
    static member Empty =
      {
        Code = StringBuilder()
        GeneratedTables = Map.empty
        GeneratedPKs = Map.empty
      }

let rec private generatePKValues
  (db : Map<string,TableDefinition>)
  (table : TableDefinition) 
  (ctxt : Context) =
  let pkFKColumns =
    table.PrimaryKey |>
    List.filter(
      fun c -> 
        table.ForeignKeys |> 
        Map.exists(fun _ v -> v |> List.map fst |> List.contains(c))
    )
  let pkColumnsNotFK =
    table.PrimaryKey |> 
    List.except pkFKColumns
  let pkNotFKValues =
    pkColumnsNotFK |>
    List.map (fun c -> 
        c.Name,c.Type.Random() ) |>
    Map.ofList

  let pkFkRefColumns =
    table.ForeignKeys |>
    Map.filter(
      fun _ refs ->
        pkFKColumns |> 
        List.exists(
          fun c -> 
            refs |> List.exists(
              fun (referencingColumn,_) -> referencingColumn = c 
          )
        )
    )

  let pkFKValues =
    pkFkRefColumns |>
    Map.fold(
      fun record table cols ->
        let recordsOfChoice =
          ctxt.GeneratedTables.[table] |>
          List.map (
            fun rs ->
              rs |> Map.filter(fun c _ -> 
                cols |> List.map snd |> List.exists (fun c1 -> c1.Name = c)
              )
          )
        let randomValues = recordsOfChoice.[random.Next(recordsOfChoice.Length)]
        let valuesWithReplacedColumnNames =
          randomValues |>
          Map.fold(
            fun (vals : Map<string,Value>) column value ->
              let referencingCol,_ =
                cols |>
                List.find(fun (_,c2) -> column = c2.Name)
              vals.Add(referencingCol.Name,value)
          ) Map.empty
        mergeMaps record valuesWithReplacedColumnNames
    ) Map.empty

  let pkValues = mergeMaps pkNotFKValues pkFKValues
  
  let pkExistingRecords =
    match ctxt.GeneratedPKs.TryFind(table.Name) with
    | Some records ->
      records|>
      List.map(
        fun cell -> 
          cell |> 
          Map.filter (
            fun fieldName _ -> 
              pkValues |>
              Map.exists(fun c _ ->
                c = fieldName)
            )
      )
    | None -> []
  if 
    pkExistingRecords.Length > 0 &&
    pkValues |> 
    Map.forall(
      fun c v ->
        pkExistingRecords |>
        List.exists(fun record ->
          record.[c] = v)
    ) 
  then
    generatePKValues db table ctxt
  else
    pkValues

and private generateStandardColumnsValues
  (table : TableDefinition) =
  
  let standardColumns =
    table.Columns |>
    List.filter (
      fun tableColumn ->
        let fkColumns =
          table.ForeignKeys |> mapItems |> List.concat
        table.PrimaryKey |> List.contains(tableColumn) |> not && 
        (
          fkColumns |> List.map fst |>
          List.contains(tableColumn) |> not)
    )

  standardColumns |>
  List.map (fun c -> c.Name,c.Type.Random()) |>
  Map.ofList

and private generateFKColumnValues
  (db : Map<string,TableDefinition>)
  (table : TableDefinition)
  (ctxt : Context) : Context * Record =

  let fkNotPK =
    table.ForeignKeys |>
    Map.filter(
      fun table cols -> 
        cols |>
        List.exists(
          fun (_,c) ->
            db.[table].PrimaryKey |> List.contains(c) |> not
        )
    ) 
  
  fkNotPK |>
  Map.fold(
    fun (ctxt : Context,values : Record) tableName _ ->
      let tablePkColumns = db.[tableName].PrimaryKey
      match ctxt.GeneratedTables.TryFind(tableName) with
      | None -> 
          raise(CodeGenerationException(sprintf "Referenced table %A should already have been filled in with values" tableName))
      | Some records ->
          let recordFKColumns =
            records |>
            List.map (
              fun r ->
                r |> Map.filter (fun c _ -> tablePkColumns |> List.exists(fun col -> col.Name = c))
            )
          let randomFKRecord = recordFKColumns.[random.Next(recordFKColumns.Length)]
          let pkValuesInFK =
            randomFKRecord |>
            Map.filter(
              fun c _ ->
                table.PrimaryKey |> List.exists(fun c1 -> c1.Name = c)
            )
          match ctxt.GeneratedPKs.TryFind(table.Name) with
          | Some records when 
              records |> List.contains(pkValuesInFK) ->
              generateFKColumnValues db table ctxt
          | _ ->
            let ctxt =
              {
                ctxt with
                  GeneratedPKs =
                    match ctxt.GeneratedPKs.TryFind(table.Name) with
                    | Some records -> ctxt.GeneratedPKs.Add(table.Name,pkValuesInFK :: records)
                    | None -> ctxt.GeneratedPKs.Add(table.Name,[pkValuesInFK])
              }
            ctxt,mergeMaps values randomFKRecord
    ) (ctxt,Map.empty)


let private generateInsertCode (table : string) (records : List<Record>) =
  let valuesCode =
    records |>
    List.map(
      fun record ->
        let recordString =
          record |> Map.toList |>
          List.map(fun (_,x) -> string x) |>
          List.reduce(fun r1 r2 -> r1 + "," + r2)
        "(" + recordString + ")"
    )
  let columnsCode =
    records.[0] |>
    Map.toList |>
    List.map (fun (c,_) -> sprintf "\"%s\"" c) |>
    List.reduce (fun c1 c2 -> c1 + "," + c2)
    
  let insertCode =
    sprintf "DELETE FROM \"%s\";\nINSERT INTO \"%s\"\n(%s)\nVALUES\n%s;\n\n"
      table table columnsCode (valuesCode |> List.reduce(fun x y -> x + ",\n" + y))
  insertCode


let rec private generateTableValues
  (db : Map<string,TableDefinition>)
  (table : TableDefinition) 
  (ctxt : Context) : Context =

  let ctxt,tableRecords =
    [1..table.Rows] |>
    List.fold(
      fun (ctxt,records : List<Record>) _ ->
        let pkValues = generatePKValues db table ctxt
        let standardValues = generateStandardColumnsValues table
        let ctxt,fkValues = generateFKColumnValues db table ctxt
        let currentRecord = mergeMaps(mergeMaps pkValues standardValues) fkValues
        let ctxt =
          { 
            ctxt with 
              GeneratedPKs = 
                match ctxt.GeneratedPKs.TryFind(table.Name) with
                | None -> ctxt.GeneratedPKs.Add(table.Name,[pkValues])
                | Some records -> ctxt.GeneratedPKs.Add(table.Name,pkValues :: records)
          }
        ctxt,currentRecord :: records
    ) (ctxt,[])
  
  let ctxt = 
    let insertCode = generateInsertCode table.Name tableRecords
    { 
      ctxt with 
        Code = ctxt.Code.Append(insertCode)
        GeneratedTables = ctxt.GeneratedTables.Add(table.Name,tableRecords) 
    }

  ctxt


let private generateDB (db : Map<string,TableDefinition>) : Context  =
  let ctxt = Context.Empty
  let independentTables = db |> Map.filter(fun _ t -> t.ForeignKeys.IsEmpty)
  let ctxt =
    independentTables |>
    Map.fold (
      fun (ctxt : Context) _ table ->
        generateTableValues db table ctxt
    ) ctxt
  let dependentTables = db |> Map.filter(fun _ t -> t.ForeignKeys.IsEmpty |> not)
  let ctxt =
    dependentTables |>
    Map.fold (
      fun (ctxt : Context) _ table ->
        generateTableValues db table ctxt
    ) ctxt
  ctxt

let compile (path : string) (fileName : string) (db : Map<string,TableDefinition>) : unit =
  if Directory.Exists path |> not && path <> "" then
    Directory.CreateDirectory path |> ignore
  let ctxt = generateDB db
  let code = ctxt.Code.ToString()
  File.WriteAllText(Path.Combine(path,fileName),code)
  
  
  



