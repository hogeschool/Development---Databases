module CompilerErrors

exception ParseError of string
exception TypeError of string

let parseError (msg : string) =
  raise(ParseError(sprintf "Parse Error: %s" msg))
