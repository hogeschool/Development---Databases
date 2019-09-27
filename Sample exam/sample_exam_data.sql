insert into "User"
values
	(1,'theowner@gmail.com','theowner',150.54,'5485484838281823'),
	(2,'progamer85@gmail.com','progamer85',25,'5485484838282234'),
	(3,'lolcat@gmail.com','lolcat',350.45,'5485226538281823'),
	(4,'powerpuff@hotmail.com','powerpuff',25.54,'1243484838281823'),
	(5,'nolife@gmail.com','nolife',0,'37.5'),
	(6,'kratos@gmail.com','kratos',55.54,'5485200038281823'),
	(7,'xyz@gmail.com','xyz',25.54,'5485484445481823'),
	(8,'sadpepe@gmail.com','sadpepe',45.20,'5545484838281823');

insert into "Game"
values
  ('Call of Duty: Modern Warfare', 2019, false, 59.99, 16),
  ('Call of Duty: Modern Warfare', 2008, true, 19.99, 16),
  ('Call of Duty: Modern Warfare 2', 2011, true, 19.99, 16),
  ('Call of Duty: Modern Warfare 3', 2012, true, 19.99, 16),
  ('Unreal Tournament 3', 2009, true, 9.99, 16),
  ('Greedfall', 2019, true, 49.99, 0),
  ('Company of Heroes 2', 2013, true, 9.99, 14),
  ('GTA V', 2017, true, 59.99, 18);

insert into "User_Game"
values
  (1, 'Call of Duty: Modern Warfare', 2008),
  (1, 'Call of Duty: Modern Warfare 2', 2011),
  (1, 'Call of Duty: Modern Warfare 3', 2012),
  (1, 'Unreal Tournament 3', 2009),
  (2, 'Greedfall', 2019),
  (2, 'Company of Heroes 2', 2013),
  (2, 'GTA V', 2017),
  (3, 'GTA V', 2017),
  (3, 'Call of Duty: Modern Warfare 2', 2011),
  (3, 'Call of Duty: Modern Warfare 3', 2012),
  (3, 'Unreal Tournament 3', 2009),
  (3, 'Greedfall', 2019),
  (4, 'Greedfall', 2019),
  (4, 'Company of Heroes 2', 2013),
  (5, 'GTA V', 2017),
  (5, 'Call of Duty: Modern Warfare 2', 2011),
  (5, 'Call of Duty: Modern Warfare', 2019),
  (6, 'Unreal Tournament 3', 2009),
  (6, 'Company of Heroes 2', 2013, 2019),
  (6, 'Call of Duty: Modern Warfare 2', 2011),
  (7, 'Greedfall', 2019),
  (8, 'GTA V', 2017),
  (8, 'Company of Heroes 2', 2013);

insert into "Review"
values
  (1,'COD2: Awesome','Just awesome',9,'Call of Duty: Modern Warfare 2', 2011,'2019-3-5'),
  (1,'COD3: Awesome','Just awesome',9,'Call of Duty: Modern Warfare 3', 2011,'2019-3-5'),
  (1,'UT3: Bad','Boring...',3,'Call of Duty: Modern Warfare 2', 2011,'2019-3-5')