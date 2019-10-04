module Databases

open DBGenerator

let userColumns : List<Column> =
  [
    { Name = "Id"; Type = Integer(1,50000) }
    { Name = "Email"; Type = Varchar(5,30) }
    { Name = "Username"; Type = Varchar(5,30) }
    { Name = "Balance"; Type = Real(0.0,1500.0) }
    { Name = "CreditCardNo"; Type = Text(16) }
  ]
let userPK = [userColumns.[0]]
let userFK = Map.empty
let user : TableDefinition =
  TableDefinition.Create("User",userColumns,userFK,userPK,100)

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
    gameColumns.[0]
    gameColumns.[1]
  ]
let gameFK = Map.empty
let game = TableDefinition.Create("Game",gameColumns,gameFK,gamePK,50)

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
        userGameColumns.[0],userColumns.[0]
      ]
    "Game",
      [
        userGameColumns.[1],gameColumns.[0]
        userGameColumns.[2],gameColumns.[1]
      ]
  ] |> Map.ofList
let userGamePK = 
  [
    userGameColumns.[0]
    userGameColumns.[1]
    userGameColumns.[2]
  ]
let userGame =
  TableDefinition.Create("User_Game",userGameColumns,userGameFK,userGamePK,50)

let VGdb =
  [
    user.Name,user
    game.Name,game
    userGame.Name,userGame
  ] |> Map.ofList
  
  

