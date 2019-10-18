module Databases

open DBGenerator
open GeneratorLanguage

let userColumns : List<Column> =
  [
    { Name = "Id"; Type = Integer(1,50000) }
    { Name = "Email"; Type = Varchar(5,30) }
    { Name = "Username"; Type = Varchar(5,30) }
    { Name = "Balance"; Type = Real(0.0,1500.0) }
    { Name = "CreditCardNo"; Type = Text(16) }
  ]
let userPK = [userColumns.[0].Name]
let userFK = Map.empty
let user : TableDefinition =
  TableDefinition.Create("User",userColumns,userFK,userPK,1000)

let gameColumns =
  [
    { Name = "Title"; Type = Varchar(5,50) }
    { Name = "Year"; Type = Integer(1980,2019) }
    { Name = "Released"; Type = Boolean }
    { Name = "Price"; Type = Real(1.0,100.0) }
    { Name = "PEGI"; Type = Integer(0,18) }
  ]
let gamePK =
  [
    gameColumns.[0].Name
    gameColumns.[1].Name
  ]
let gameFK = Map.empty
let game = TableDefinition.Create("Game",gameColumns,gameFK,gamePK,500)

let userGameColumns =
  [
    { Name = "UserId"; Type = Integer(1,50000) }
    { Name = "Title"; Type = Varchar(5,50)  }
    { Name = "Year"; Type = Integer(1980,2019) }   
  ]
let userGameFK =
  [
    "User",
      [
        userGameColumns.[0].Name,userColumns.[0].Name
      ]
    "Game",
      [
        userGameColumns.[1].Name,gameColumns.[0].Name
        userGameColumns.[2].Name,gameColumns.[1].Name
      ]
  ] |> Map.ofList
let userGamePK = 
  [
    userGameColumns.[0].Name
    userGameColumns.[1].Name
    userGameColumns.[2].Name
  ]
let userGame =
  TableDefinition.Create("User_Game",userGameColumns,userGameFK,userGamePK,250)

let VGdb =
  [
    user.Name,user
    game.Name,game
    userGame.Name,userGame
  ] |> Map.ofList
  
  

