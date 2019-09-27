module TableGenerator

open System.Text
open System
open Utils

let random = Random() 

exception CodeGenerationException of string

type Value =
| Integer of int
| Text of string
| Date of string
| Real of float


type SQLType =
| Integer of int * int
| Varchar of int * int
| Char of int
| Text of int
| Date
| Real of float * float
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
    fun values tableName fkColumns ->
      match ctxt.GeneratedTables.TryFind(tableName) with
      | None -> 
          raise(CodeGenerationException(sprintf "Referenced table %A should already have been filled in with values" tableName))
      | Some records ->
          let recordFKColumns =
            records |>
            List.map (
              fun r ->
                r |> Map.filter (fun c _ -> fkColumns |> List.contains(c))
            )
  ) Map.empty


let rec generateTable 
  (db : Map<string,TableDefinition>) 
  (table : TableDefinition) 
  (ctxt : Context) : Context =

  let tableRecords =

    [1..table.Rows] |>
    List.fold(
      fun (records : List<Record>) _ ->
        let pkValues = generatePKValues table ctxt
        let standardColumns = generateStandardColumnsValues table
    ) []
  

  failwith "not implemented"
      
    



let generateDB (db : Map<string,TableDefinition>) : Context  =
  let ctxt = Context.Empty
  let independentTables = db |> Map.filter(fun _ t -> t.ForeignKeys.IsEmpty)
  let ctxt =
    independentTables |>
    Map.fold (
      fun (ctxt : Context) _ table ->
        { ctxt with Code = ctxt.Code.Append(generateTable db table ctxt) }
    ) ctxt
  let dependentTables = db |> Map.filter(fun _ t -> t.ForeignKeys.IsEmpty |> not)
  let ctxt =
    dependentTables |>
    Map.fold (
      fun (code : Context) _ table ->
        { ctxt with Code = ctxt.Code.Append(generateTable db table code) }
    ) ctxt
  ctxt



