module Compiler

open FSharp.Text.Lexing
open System.IO
open ParserUtils

type CompilerOptions =
| File of string
| String of string

let private parseFile (filePath : string) =
    let inputChannel = new StreamReader(filePath)
    let lexbuf = LexBuffer<char>.FromTextReader inputChannel
    let ast = Parser.start Lexer.tokenstream lexbuf
    let db = generateDb ast 
    db

let private parseString (program : string) =
  let lexbuf = LexBuffer<char>.FromString program
  let ast = Parser.start Lexer.tokenstream lexbuf
  let db = generateDb ast 
  db

let compile 
  (options : CompilerOptions) =

  match options with
  | File f -> parseFile f
  | String s -> parseString s

