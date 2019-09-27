module TableGenerator

open System.Text
open System
open Utils

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
    | Text s -> s
    | Boolean b -> if b then "true" else "false"


type SQLType =
| Integer of int * int
| Varchar of int * int
| Char of int
| Text of int
| Date
| Real of float * float
| Boolean
  with
    member this.Random : Value =
      failwith "not implemented"

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

type ForeignKeys = Map<string,List<Column>>

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

type Record = Map<string,Value>

type Context =
  {
    Code                : StringBuilder
    GeneratedTables     : Map<string,List<Record>>
  }
  with
    static member Empty =
      {
        Code = StringBuilder()
        GeneratedTables = Map.empty
      }

let getRecordColumnValues (column : string) (records : List<Record>) : List<Value> =
  records |>
  List.fold(
    fun values records ->
      records.[column] :: values
  ) []

let rec generatePKValues (table : TableDefinition) (ctxt : Context) =
  let pkValues =
    table.PrimaryKey |>
    List.map ( fun c -> c.Name,c.Type.Random ) |>
    Map.ofList
  let pkExistingRecords =
    ctxt.GeneratedTables.[table.Name] |>
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
  if 
    pkValues |> 
    Map.forall(
      fun c v ->
        pkExistingRecords |>
        List.exists(fun record ->
          record.[c] = v)
    ) 
  then
    generatePKValues table ctxt
  else
    pkValues

let generateStandardColumnsValues
  (table : TableDefinition) =
  
  let standardColumns =
    table.Columns |>
    List.filter (
      fun tableColumn ->
        let fkColumns =
          table.ForeignKeys |> mapItems |> List.concat
        table.PrimaryKey |> List.contains(tableColumn) |> not && 
        (
          fkColumns |>
          List.contains(tableColumn) |> not)
    )

  standardColumns |>
  List.map (fun c -> c.Name,c.Type.Random) |>
  Map.ofList

let generateFKColumnValues
  (table : TableDefinition)
  (ctxt : Context) =
  
  table.ForeignKeys |>
  Map.fold(
    fun (values : Record) tableName fkColumns ->
      match ctxt.GeneratedTables.TryFind(tableName) with
      | None -> 
          raise(CodeGenerationException(sprintf "Referenced table %A should already have been filled in with values" tableName))
      | Some records ->
          let recordFKColumns =
            records |>
            List.map (
              fun r ->
                r |> Map.filter (fun c _ -> fkColumns |> List.exists(fun col -> col.Name = c))
            )
          let fkValueRandomValues =
            fkColumns |>
            List.fold(
              fun (record : Record) (c : Column) ->
                let valuesOfChoice = getRecordColumnValues c.Name recordFKColumns
                let randomValue = randomListElement random valuesOfChoice
                match randomValue with
                | None -> raise(CodeGenerationException(sprintf "No values found for foreign key column %A" c))
                | Some v -> record.Add(c.Name,v)
            ) Map.empty
          mergeMaps values fkValueRandomValues
    ) Map.empty


let generateInsertCode (table : string) (records : List<Record>) =
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

  sprintf "INSERT INTO %s\nVALUES\n%s;"
    table (valuesCode |> List.reduce(fun x y -> x + ",\n" + y))


let rec generateTableValues 
  (table : TableDefinition) 
  (ctxt : Context) : Context =

  let tableRecords =
    [1..table.Rows] |>
    List.fold(
      fun (records : List<Record>) _ ->
        let pkValues = generatePKValues table ctxt
        let standardValues = generateStandardColumnsValues table
        let fkValues = generateFKColumnValues table ctxt
        let currentRecord = mergeMaps(mergeMaps pkValues standardValues) fkValues
        currentRecord :: records
    ) []
  
  let ctxt = 
    let insertCode = generateInsertCode table.Name tableRecords
    { 
      ctxt with 
        Code = ctxt.Code.Append(insertCode)
        GeneratedTables = ctxt.GeneratedTables.Add(table.Name,tableRecords) 
    }

  ctxt


      
    



let generateDB (db : Map<string,TableDefinition>) : Context  =
  let ctxt = Context.Empty
  let independentTables = db |> Map.filter(fun _ t -> t.ForeignKeys.IsEmpty)
  let ctxt =
    independentTables |>
    Map.fold (
      fun (ctxt : Context) _ table ->
        { ctxt with Code = ctxt.Code.Append(generateTableValues table ctxt) }
    ) ctxt
  let dependentTables = db |> Map.filter(fun _ t -> t.ForeignKeys.IsEmpty |> not)
  let ctxt =
    dependentTables |>
    Map.fold (
      fun (code : Context) _ table ->
        { ctxt with Code = ctxt.Code.Append(generateTableValues table code) }
    ) ctxt
  ctxt



