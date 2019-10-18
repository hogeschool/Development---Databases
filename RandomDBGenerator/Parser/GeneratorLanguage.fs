module GeneratorLanguage

open System
open System.Text

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

let private getRandomString (length : int) (random : Random) =
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
    member this.Random(random : Random) : Value =
      match this with
      | Integer (min,max) -> Value.Integer (random.Next(min,max + 1))
      | Real (min,max) -> Value.Real (min + random.NextDouble() * max)
      | Varchar (min,max) ->
          let length = random.Next(min,max + 1)
          Value.Text (getRandomString length random)
      | Text length -> 
          Value.Text (getRandomString length random)
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

type ForeignKeys = Map<string,List<string * string>>

and TableDefinition =
  {
    Name            : string
    Columns         : List<Column>
    ForeignKeys     : ForeignKeys
    PrimaryKey      : List<string>
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
    static member Empty =
      {
        Name = ""
        Columns = []
        ForeignKeys = Map.empty
        PrimaryKey = []
        Rows = 0
      }



