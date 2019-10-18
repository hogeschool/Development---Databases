// Learn more about F# at http://fsharp.org

open DBGenerator
open Databases
open Compiler

[<EntryPoint>]
let main argv =
  let compiledProgram = compile (CompilerOptions.File argv.[0])
  printfn "%d" compiledProgram
  //generate "" "test.sql" VGdb
  0
