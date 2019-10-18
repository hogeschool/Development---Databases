// Signature file for parser generated by fsyacc
module Parser
type token = 
  | EOF
  | TABLE
  | WITH
  | ROWS
  | FOREIGN
  | PRIMARY
  | KEY
  | REFERENCES
  | COMMA
  | LPAR
  | RPAR
  | LRANGE
  | RRANGE
  | SEMICOLON
  | TYPE_INTEGER
  | TYPE_VARCHAR
  | TYPE_TEXT
  | TYPE_REAL
  | TYPE_BOOLEAN
  | TYPE_DATE
  | DATE of ( string )
  | BOOLEAN of ( bool )
  | STRING of (System.String)
  | ID of (System.String)
  | DECIMAL of (System.Double)
  | INT of (System.Int32)
type tokenId = 
    | TOKEN_EOF
    | TOKEN_TABLE
    | TOKEN_WITH
    | TOKEN_ROWS
    | TOKEN_FOREIGN
    | TOKEN_PRIMARY
    | TOKEN_KEY
    | TOKEN_REFERENCES
    | TOKEN_COMMA
    | TOKEN_LPAR
    | TOKEN_RPAR
    | TOKEN_LRANGE
    | TOKEN_RRANGE
    | TOKEN_SEMICOLON
    | TOKEN_TYPE_INTEGER
    | TOKEN_TYPE_VARCHAR
    | TOKEN_TYPE_TEXT
    | TOKEN_TYPE_REAL
    | TOKEN_TYPE_BOOLEAN
    | TOKEN_TYPE_DATE
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
    | NONTERM_sqlTypeRange
    | NONTERM_columnDef
    | NONTERM_columns
    | NONTERM_idSeq
    | NONTERM_foreignKey
    | NONTERM_foreignKeys
    | NONTERM_foreignKeySeq
    | NONTERM_tableDefinition
    | NONTERM_tableDefinitions
/// This function maps tokens to integer indexes
val tagOfToken: token -> int

/// This function maps integer indexes to symbolic token ids
val tokenTagToTokenId: int -> tokenId

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
val prodIdxToNonTerminal: int -> nonTerminalId

/// This function gets the name of a token as a string
val token_to_string: token -> string
val start : (FSharp.Text.Lexing.LexBuffer<'cty> -> token) -> FSharp.Text.Lexing.LexBuffer<'cty> -> ( List<GeneratorLanguage.TableDefinition> ) 
