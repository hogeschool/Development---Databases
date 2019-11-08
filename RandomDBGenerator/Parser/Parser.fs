// Implementation file for parser generated by fsyacc
module Parser
#nowarn "64";; // turn off warnings that type variables used in production annotations are instantiated to concrete type
open FSharp.Text.Lexing
open FSharp.Text.Parsing.ParseHelpers
# 1 "Parser.fsy"

  open GeneratorLanguage 

# 10 "Parser.fs"
// This type is the type of tokens accepted by the parser
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
// This type is used to give symbolic names to token indexes, useful for error messages
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
// This type is used to give symbolic names to token indexes, useful for error messages
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

// This function maps tokens to integer indexes
let tagOfToken (t:token) = 
  match t with
  | EOF  -> 0 
  | TABLE  -> 1 
  | WITH  -> 2 
  | ROWS  -> 3 
  | FOREIGN  -> 4 
  | PRIMARY  -> 5 
  | KEY  -> 6 
  | REFERENCES  -> 7 
  | COMMA  -> 8 
  | LPAR  -> 9 
  | RPAR  -> 10 
  | LRANGE  -> 11 
  | RRANGE  -> 12 
  | SEMICOLON  -> 13 
  | TYPE_INTEGER  -> 14 
  | TYPE_VARCHAR  -> 15 
  | TYPE_TEXT  -> 16 
  | TYPE_REAL  -> 17 
  | TYPE_BOOLEAN  -> 18 
  | TYPE_DATE  -> 19 
  | DATE _ -> 20 
  | BOOLEAN _ -> 21 
  | STRING _ -> 22 
  | ID _ -> 23 
  | DECIMAL _ -> 24 
  | INT _ -> 25 

// This function maps integer indexes to symbolic token ids
let tokenTagToTokenId (tokenIdx:int) = 
  match tokenIdx with
  | 0 -> TOKEN_EOF 
  | 1 -> TOKEN_TABLE 
  | 2 -> TOKEN_WITH 
  | 3 -> TOKEN_ROWS 
  | 4 -> TOKEN_FOREIGN 
  | 5 -> TOKEN_PRIMARY 
  | 6 -> TOKEN_KEY 
  | 7 -> TOKEN_REFERENCES 
  | 8 -> TOKEN_COMMA 
  | 9 -> TOKEN_LPAR 
  | 10 -> TOKEN_RPAR 
  | 11 -> TOKEN_LRANGE 
  | 12 -> TOKEN_RRANGE 
  | 13 -> TOKEN_SEMICOLON 
  | 14 -> TOKEN_TYPE_INTEGER 
  | 15 -> TOKEN_TYPE_VARCHAR 
  | 16 -> TOKEN_TYPE_TEXT 
  | 17 -> TOKEN_TYPE_REAL 
  | 18 -> TOKEN_TYPE_BOOLEAN 
  | 19 -> TOKEN_TYPE_DATE 
  | 20 -> TOKEN_DATE 
  | 21 -> TOKEN_BOOLEAN 
  | 22 -> TOKEN_STRING 
  | 23 -> TOKEN_ID 
  | 24 -> TOKEN_DECIMAL 
  | 25 -> TOKEN_INT 
  | 28 -> TOKEN_end_of_input
  | 26 -> TOKEN_error
  | _ -> failwith "tokenTagToTokenId: bad token"

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
let prodIdxToNonTerminal (prodIdx:int) = 
  match prodIdx with
    | 0 -> NONTERM__startstart 
    | 1 -> NONTERM_start 
    | 2 -> NONTERM_sqlTypeRange 
    | 3 -> NONTERM_sqlTypeRange 
    | 4 -> NONTERM_sqlTypeRange 
    | 5 -> NONTERM_sqlTypeRange 
    | 6 -> NONTERM_columnDef 
    | 7 -> NONTERM_columnDef 
    | 8 -> NONTERM_columnDef 
    | 9 -> NONTERM_columns 
    | 10 -> NONTERM_columns 
    | 11 -> NONTERM_idSeq 
    | 12 -> NONTERM_idSeq 
    | 13 -> NONTERM_foreignKey 
    | 14 -> NONTERM_foreignKeys 
    | 15 -> NONTERM_foreignKeys 
    | 16 -> NONTERM_foreignKeySeq 
    | 17 -> NONTERM_foreignKeySeq 
    | 18 -> NONTERM_tableDefinition 
    | 19 -> NONTERM_tableDefinitions 
    | 20 -> NONTERM_tableDefinitions 
    | 21 -> NONTERM_tableDefinitions 
    | _ -> failwith "prodIdxToNonTerminal: bad production index"

let _fsyacc_endOfInputTag = 28 
let _fsyacc_tagOfErrorTerminal = 26

// This function gets the name of a token as a string
let token_to_string (t:token) = 
  match t with 
  | EOF  -> "EOF" 
  | TABLE  -> "TABLE" 
  | WITH  -> "WITH" 
  | ROWS  -> "ROWS" 
  | FOREIGN  -> "FOREIGN" 
  | PRIMARY  -> "PRIMARY" 
  | KEY  -> "KEY" 
  | REFERENCES  -> "REFERENCES" 
  | COMMA  -> "COMMA" 
  | LPAR  -> "LPAR" 
  | RPAR  -> "RPAR" 
  | LRANGE  -> "LRANGE" 
  | RRANGE  -> "RRANGE" 
  | SEMICOLON  -> "SEMICOLON" 
  | TYPE_INTEGER  -> "TYPE_INTEGER" 
  | TYPE_VARCHAR  -> "TYPE_VARCHAR" 
  | TYPE_TEXT  -> "TYPE_TEXT" 
  | TYPE_REAL  -> "TYPE_REAL" 
  | TYPE_BOOLEAN  -> "TYPE_BOOLEAN" 
  | TYPE_DATE  -> "TYPE_DATE" 
  | DATE _ -> "DATE" 
  | BOOLEAN _ -> "BOOLEAN" 
  | STRING _ -> "STRING" 
  | ID _ -> "ID" 
  | DECIMAL _ -> "DECIMAL" 
  | INT _ -> "INT" 

// This function gets the data carried by a token as an object
let _fsyacc_dataOfToken (t:token) = 
  match t with 
  | EOF  -> (null : System.Object) 
  | TABLE  -> (null : System.Object) 
  | WITH  -> (null : System.Object) 
  | ROWS  -> (null : System.Object) 
  | FOREIGN  -> (null : System.Object) 
  | PRIMARY  -> (null : System.Object) 
  | KEY  -> (null : System.Object) 
  | REFERENCES  -> (null : System.Object) 
  | COMMA  -> (null : System.Object) 
  | LPAR  -> (null : System.Object) 
  | RPAR  -> (null : System.Object) 
  | LRANGE  -> (null : System.Object) 
  | RRANGE  -> (null : System.Object) 
  | SEMICOLON  -> (null : System.Object) 
  | TYPE_INTEGER  -> (null : System.Object) 
  | TYPE_VARCHAR  -> (null : System.Object) 
  | TYPE_TEXT  -> (null : System.Object) 
  | TYPE_REAL  -> (null : System.Object) 
  | TYPE_BOOLEAN  -> (null : System.Object) 
  | TYPE_DATE  -> (null : System.Object) 
  | DATE _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | BOOLEAN _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | STRING _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | ID _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | DECIMAL _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | INT _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
let _fsyacc_gotos = [| 0us; 65535us; 1us; 65535us; 0us; 1us; 1us; 65535us; 28us; 32us; 2us; 65535us; 34us; 33us; 53us; 33us; 2us; 65535us; 34us; 35us; 53us; 54us; 3us; 65535us; 37us; 38us; 41us; 42us; 59us; 60us; 2us; 65535us; 47us; 46us; 55us; 46us; 2us; 65535us; 47us; 48us; 55us; 49us; 1us; 65535us; 55us; 56us; 2us; 65535us; 0us; 66us; 67us; 66us; 2us; 65535us; 0us; 2us; 67us; 68us; |]
let _fsyacc_sparseGotoTableRowOffsets = [|0us; 1us; 3us; 5us; 8us; 11us; 15us; 18us; 21us; 23us; 26us; |]
let _fsyacc_stateToProdIdxsTableElements = [| 1us; 0us; 1us; 0us; 1us; 1us; 1us; 1us; 1us; 2us; 1us; 2us; 1us; 2us; 1us; 2us; 1us; 2us; 1us; 2us; 1us; 3us; 1us; 3us; 1us; 3us; 1us; 3us; 1us; 3us; 1us; 3us; 1us; 4us; 1us; 4us; 1us; 4us; 1us; 4us; 1us; 4us; 1us; 4us; 1us; 5us; 1us; 5us; 1us; 5us; 1us; 5us; 1us; 5us; 1us; 5us; 3us; 6us; 7us; 8us; 1us; 6us; 1us; 6us; 1us; 7us; 1us; 8us; 2us; 9us; 10us; 1us; 9us; 1us; 9us; 2us; 11us; 12us; 1us; 11us; 1us; 11us; 1us; 13us; 1us; 13us; 1us; 13us; 1us; 13us; 1us; 13us; 1us; 13us; 1us; 13us; 2us; 14us; 15us; 1us; 14us; 1us; 14us; 1us; 16us; 1us; 16us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 1us; 18us; 2us; 19us; 20us; 1us; 19us; 1us; 19us; |]
let _fsyacc_stateToProdIdxsTableRowOffsets = [|0us; 2us; 4us; 6us; 8us; 10us; 12us; 14us; 16us; 18us; 20us; 22us; 24us; 26us; 28us; 30us; 32us; 34us; 36us; 38us; 40us; 42us; 44us; 46us; 48us; 50us; 52us; 54us; 56us; 60us; 62us; 64us; 66us; 68us; 71us; 73us; 75us; 78us; 80us; 82us; 84us; 86us; 88us; 90us; 92us; 94us; 96us; 99us; 101us; 103us; 105us; 107us; 109us; 111us; 113us; 115us; 117us; 119us; 121us; 123us; 125us; 127us; 129us; 131us; 133us; 135us; 137us; 140us; 142us; |]
let _fsyacc_action_rows = 69
let _fsyacc_actionTableElements = [|1us; 16405us; 1us; 51us; 0us; 49152us; 1us; 32768us; 0us; 3us; 0us; 16385us; 1us; 32768us; 11us; 5us; 1us; 32768us; 25us; 6us; 1us; 32768us; 8us; 7us; 1us; 32768us; 25us; 8us; 1us; 32768us; 12us; 9us; 0us; 16386us; 1us; 32768us; 11us; 11us; 1us; 32768us; 25us; 12us; 1us; 32768us; 8us; 13us; 1us; 32768us; 25us; 14us; 1us; 32768us; 12us; 15us; 0us; 16387us; 1us; 32768us; 11us; 17us; 1us; 32768us; 24us; 18us; 1us; 32768us; 8us; 19us; 1us; 32768us; 24us; 20us; 1us; 32768us; 12us; 21us; 0us; 16388us; 1us; 32768us; 11us; 23us; 1us; 32768us; 25us; 24us; 1us; 32768us; 8us; 25us; 1us; 32768us; 25us; 26us; 1us; 32768us; 12us; 27us; 0us; 16389us; 6us; 32768us; 14us; 4us; 15us; 10us; 16us; 29us; 17us; 16us; 18us; 31us; 19us; 22us; 1us; 32768us; 25us; 30us; 0us; 16390us; 0us; 16391us; 0us; 16392us; 1us; 16394us; 8us; 34us; 1us; 32768us; 23us; 28us; 0us; 16393us; 1us; 16396us; 8us; 37us; 1us; 32768us; 23us; 36us; 0us; 16395us; 1us; 32768us; 6us; 40us; 1us; 32768us; 9us; 41us; 1us; 32768us; 23us; 36us; 1us; 32768us; 10us; 43us; 1us; 32768us; 7us; 44us; 1us; 32768us; 23us; 45us; 0us; 16397us; 1us; 16399us; 8us; 47us; 1us; 32768us; 4us; 39us; 0us; 16398us; 1us; 32768us; 13us; 50us; 0us; 16400us; 1us; 32768us; 23us; 52us; 1us; 32768us; 9us; 53us; 1us; 32768us; 23us; 28us; 1us; 32768us; 13us; 55us; 1us; 16401us; 4us; 39us; 1us; 32768us; 5us; 57us; 1us; 32768us; 6us; 58us; 1us; 32768us; 9us; 59us; 1us; 32768us; 23us; 36us; 1us; 32768us; 10us; 61us; 1us; 32768us; 10us; 62us; 1us; 32768us; 2us; 63us; 1us; 32768us; 3us; 64us; 1us; 32768us; 25us; 65us; 0us; 16402us; 1us; 16404us; 13us; 67us; 1us; 16405us; 1us; 51us; 0us; 16403us; |]
let _fsyacc_actionTableRowOffsets = [|0us; 2us; 3us; 5us; 6us; 8us; 10us; 12us; 14us; 16us; 17us; 19us; 21us; 23us; 25us; 27us; 28us; 30us; 32us; 34us; 36us; 38us; 39us; 41us; 43us; 45us; 47us; 49us; 50us; 57us; 59us; 60us; 61us; 62us; 64us; 66us; 67us; 69us; 71us; 72us; 74us; 76us; 78us; 80us; 82us; 84us; 85us; 87us; 89us; 90us; 92us; 93us; 95us; 97us; 99us; 101us; 103us; 105us; 107us; 109us; 111us; 113us; 115us; 117us; 119us; 121us; 122us; 124us; 126us; |]
let _fsyacc_reductionSymbolCounts = [|1us; 2us; 6us; 6us; 6us; 6us; 3us; 2us; 2us; 3us; 1us; 3us; 1us; 7us; 3us; 1us; 2us; 0us; 15us; 3us; 1us; 0us; |]
let _fsyacc_productionToNonTerminalTable = [|0us; 1us; 2us; 2us; 2us; 2us; 3us; 3us; 3us; 4us; 4us; 5us; 5us; 6us; 7us; 7us; 8us; 8us; 9us; 10us; 10us; 10us; |]
let _fsyacc_immediateActions = [|65535us; 49152us; 65535us; 16385us; 65535us; 65535us; 65535us; 65535us; 65535us; 16386us; 65535us; 65535us; 65535us; 65535us; 65535us; 16387us; 65535us; 65535us; 65535us; 65535us; 65535us; 16388us; 65535us; 65535us; 65535us; 65535us; 65535us; 16389us; 65535us; 65535us; 16390us; 16391us; 16392us; 65535us; 65535us; 16393us; 65535us; 65535us; 16395us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16397us; 65535us; 65535us; 16398us; 65535us; 16400us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16402us; 65535us; 65535us; 16403us; |]
let _fsyacc_reductions ()  =    [| 
# 246 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data :  List<GeneratorLanguage.TableDefinition> )) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
                      raise (FSharp.Text.Parsing.Accept(Microsoft.FSharp.Core.Operators.box _1))
                   )
                 : '_startstart));
# 255 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'tableDefinitions)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 34 "Parser.fsy"
                                                   _1 
                   )
# 34 "Parser.fsy"
                 :  List<GeneratorLanguage.TableDefinition> ));
# 266 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 37 "Parser.fsy"
                                                                   SQLType.Integer(_3,_5) 
                   )
# 37 "Parser.fsy"
                 : 'sqlTypeRange));
# 278 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 38 "Parser.fsy"
                                                                   SQLType.Varchar(_3,_5) 
                   )
# 38 "Parser.fsy"
                 : 'sqlTypeRange));
# 290 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Double)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : System.Double)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 39 "Parser.fsy"
                                                                           SQLType.Real(_3,_5) 
                   )
# 39 "Parser.fsy"
                 : 'sqlTypeRange));
# 302 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 40 "Parser.fsy"
                                                                   SQLType.Date(_3,_5) 
                   )
# 40 "Parser.fsy"
                 : 'sqlTypeRange));
# 314 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : System.String)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 43 "Parser.fsy"
                                          Column.Create(_1,SQLType.Text _3) 
                   )
# 43 "Parser.fsy"
                 : 'columnDef));
# 326 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : System.String)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 44 "Parser.fsy"
                                         Column.Create(_1,SQLType.Boolean) 
                   )
# 44 "Parser.fsy"
                 : 'columnDef));
# 337 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : System.String)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'sqlTypeRange)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 45 "Parser.fsy"
                                         Column.Create(_1,_2) 
                   )
# 45 "Parser.fsy"
                 : 'columnDef));
# 349 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'columnDef)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'columns)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 48 "Parser.fsy"
                                                 _1 :: _3 
                   )
# 48 "Parser.fsy"
                 : 'columns));
# 361 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'columnDef)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 49 "Parser.fsy"
                                   [_1] 
                   )
# 49 "Parser.fsy"
                 : 'columns));
# 372 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : System.String)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'idSeq)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 52 "Parser.fsy"
                                        _1 :: _3 
                   )
# 52 "Parser.fsy"
                 : 'idSeq));
# 384 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : System.String)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 53 "Parser.fsy"
                            [_1] 
                   )
# 53 "Parser.fsy"
                 : 'idSeq));
# 395 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : 'idSeq)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : System.String)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 56 "Parser.fsy"
                                                                   
                         let rawReferences = _4 |> List.map (fun x -> (x,""))
                         _7,rawReferences
                       
                   )
# 56 "Parser.fsy"
                 : 'foreignKey));
# 410 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'foreignKey)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'foreignKeys)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 62 "Parser.fsy"
                                                      _1 :: _3 
                   )
# 62 "Parser.fsy"
                 : 'foreignKeys));
# 422 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'foreignKey)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 63 "Parser.fsy"
                                    [_1] 
                   )
# 63 "Parser.fsy"
                 : 'foreignKeys));
# 433 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'foreignKeys)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 66 "Parser.fsy"
                                               _1 
                   )
# 66 "Parser.fsy"
                 : 'foreignKeySeq));
# 444 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 67 "Parser.fsy"
                         [] 
                   )
# 67 "Parser.fsy"
                 : 'foreignKeySeq));
# 454 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : System.String)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : 'columns)) in
            let _6 = (let data = parseState.GetInput(6) in (Microsoft.FSharp.Core.Operators.unbox data : 'foreignKeySeq)) in
            let _10 = (let data = parseState.GetInput(10) in (Microsoft.FSharp.Core.Operators.unbox data : 'idSeq)) in
            let _15 = (let data = parseState.GetInput(15) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 71 "Parser.fsy"
                                                                                                                     
                         let fkMap = _6 |> Map.ofList
                         TableDefinition.Create(_2,_4,fkMap,_10,_15)
                       
                   )
# 71 "Parser.fsy"
                 : 'tableDefinition));
# 472 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'tableDefinition)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'tableDefinitions)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 77 "Parser.fsy"
                                                                    _1 :: _3 
                   )
# 77 "Parser.fsy"
                 : 'tableDefinitions));
# 484 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'tableDefinition)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 78 "Parser.fsy"
                                         [_1] 
                   )
# 78 "Parser.fsy"
                 : 'tableDefinitions));
# 495 "Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 79 "Parser.fsy"
                         [] 
                   )
# 79 "Parser.fsy"
                 : 'tableDefinitions));
|]
# 506 "Parser.fs"
let tables () : FSharp.Text.Parsing.Tables<_> = 
  { reductions= _fsyacc_reductions ();
    endOfInputTag = _fsyacc_endOfInputTag;
    tagOfToken = tagOfToken;
    dataOfToken = _fsyacc_dataOfToken; 
    actionTableElements = _fsyacc_actionTableElements;
    actionTableRowOffsets = _fsyacc_actionTableRowOffsets;
    stateToProdIdxsTableElements = _fsyacc_stateToProdIdxsTableElements;
    stateToProdIdxsTableRowOffsets = _fsyacc_stateToProdIdxsTableRowOffsets;
    reductionSymbolCounts = _fsyacc_reductionSymbolCounts;
    immediateActions = _fsyacc_immediateActions;
    gotos = _fsyacc_gotos;
    sparseGotoTableRowOffsets = _fsyacc_sparseGotoTableRowOffsets;
    tagOfErrorTerminal = _fsyacc_tagOfErrorTerminal;
    parseError = (fun (ctxt:FSharp.Text.Parsing.ParseErrorContext<_>) -> 
                              match parse_error_rich with 
                              | Some f -> f ctxt
                              | None -> parse_error ctxt.Message);
    numTerminals = 29;
    productionToNonTerminalTable = _fsyacc_productionToNonTerminalTable  }
let engine lexer lexbuf startState = (tables ()).Interpret(lexer, lexbuf, startState)
let start lexer lexbuf :  List<GeneratorLanguage.TableDefinition>  =
    Microsoft.FSharp.Core.Operators.unbox ((tables ()).Interpret(lexer, lexbuf, 0))
