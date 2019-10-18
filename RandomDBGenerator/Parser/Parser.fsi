// Signature file for parser generated by fsyacc
module Parser
type token = 
  | EOF
  | DATE of ( string )
  | BOOLEAN of ( bool )
  | STRING of (System.String)
  | ID of (System.String)
  | DECIMAL of (System.Double)
  | INT of (System.Int32)
type tokenId = 
    | TOKEN_EOF
    | TOKEN_DATE
    | TOKEN_BOOLEAN
    | TOKEN_STRING
    | TOKEN_ID
    | TOKEN_DECIMAL
    | TOKEN_INT
    | TOKEN_end_of_input
    | TOKEN_error
type nonTerminalId = 
    | NONTERM__startstart
    | NONTERM_start
/// This function maps tokens to integer indexes
val tagOfToken: token -> int

/// This function maps integer indexes to symbolic token ids
val tokenTagToTokenId: int -> tokenId

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
val prodIdxToNonTerminal: int -> nonTerminalId

/// This function gets the name of a token as a string
val token_to_string: token -> string
val start : (FSharp.Text.Lexing.LexBuffer<'cty> -> token) -> FSharp.Text.Lexing.LexBuffer<'cty> -> ( int ) 
