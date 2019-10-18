module Compiler

open FSharp.Text.Lexing
open System.IO

type CompilerOptions =
| File of string
| String of string

let private parseFile (filePath : string) =
    let inputChannel = new StreamReader(filePath)
    let lexbuf = LexBuffer<char>.FromTextReader inputChannel
    let parsed = Parser.start Lexer.tokenstream lexbuf
    parsed

let private parseString (program : string) =
  let lexbuf = LexBuffer<char>.FromString program
  let parsed = Parser.start Lexer.tokenstream lexbuf
  parsed

let compile 
  (options : CompilerOptions) =

  match options with
  | File f -> parseFile f
  | String s -> parseString s

