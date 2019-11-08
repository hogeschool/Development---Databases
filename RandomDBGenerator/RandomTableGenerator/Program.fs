// Learn more about F# at http://fsharp.org

open DBGenerator
<<<<<<< HEAD
open Databases

[<EntryPoint>]
let main argv =
  compile "" "test.sql" VGdb
=======
open Compiler

exception CommandLineException of string

[<EntryPoint>]
let main argv =
  let input =
    if argv.Length > 0 && argv.Length < 3 then
      CompilerOptions.File argv.[0]
    elif argv.[0] = "-f" then
      CompilerOptions.File argv.[1]
    elif argv.[0] = "-s" then
      CompilerOptions.String argv.[1]
    else
      raise(CommandLineException 
        "Usage: -f <fileName> <outputDir> <outputFile>| 
        -s <program string> <outputDir> <outputFile>")
  compile input argv.[2] argv.[3]
>>>>>>> feature/database-generator-parser
  0
