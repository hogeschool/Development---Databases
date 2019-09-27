// Learn more about F# at http://fsharp.org

open DBGenerator
open Databases

[<EntryPoint>]
let main argv =
  compile "" "test.sql" VGdb
  0
