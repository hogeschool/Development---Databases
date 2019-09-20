create table "User" (
	"Id" integer primary key,
	"Email" varchar(30) unique,
	"Username" varchar(30) unique,
	"Balance" real check ("Balance" > 0),
	"CreditCardNo" char(16)
);

create table "Game" (
	"Title" varchar(50),
	"Year" integer,
  "Published" boolean,
	"Price" real check ("Price" > 0),
	"PEGI" integer check ("PEGI" > 0),
	primary key("Title", "Year")
);

create table "User_Game" (
	"UserId" integer,
	"Title" varchar(50),
	"Publisher" varchar(20),
	"Year" integer,
	primary key ("UserId", "Title", "Publisher", "Year"),
  foreign key ("UserId") references "User" on delete cascade,
  foreign key ("Title", "Publisher", "Year") references "Game" on delete cascade
);

create table "Review"  (
  "UserId" integer not null references "User" on delete cascade,
  "Title" varchar(50),
  "Text" text,
  "Score" smallint check ("Score" between 0 and 10),
  "GameId" integer not null references "User" on delete cascade,
  "Date" timestamp without time zone, 
  primary key ("UserId", "Title")
);