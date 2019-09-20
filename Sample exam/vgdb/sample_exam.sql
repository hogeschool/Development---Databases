-- question 1
create table "Producer" (
  "Id" integer primary key,
  "Name" varchar(20) not null,
  "Founded" integer,
  check ("Founded" > 1990)
);

create table "Preview" (
  "PreviewId" integer,
  "GameId" integer,
  "PreviewVideo" text,
  primary key("PreviewId", "GameId"),
  foreign key ("GameId") references "Game"
)