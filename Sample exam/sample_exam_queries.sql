-- question 4

select distinct "Title","Price"
from "Game"
where "Year" > 2010 and "Released" = true;

-- question 5

select g."Title",g."Price"
from 
	"User" as u 
	inner join "User_Game" as ug on u."Id" = ug."UserId"
	inner join "Game" as g on g."Title" = ug."Title" and g."Year" = ug."Year"
where u."Username" = 'pureownage';

-- question 6
select u."Username",avg(r."Score")
from
	"User" as u 
	inner join "User_Game" as ug on u."Id" = ug."UserId"
	inner join "Game" as g on g."Title" = ug."Title" and g."Year" = ug."Year"
	inner join 
		"Review" as r on r."GameTitle" = g."Title" and r."GameYear" = g."Year" and
		r."UserId" = u."Id"
group by u."Username";

-- question 7

with game_review as (
	select avg (r."Score")
	from 
		"Game" as g
		inner join "Review" as r on g."Title" = r."GameTitle" and g."Year" = r."GameYear"
	group by g."Title",g."Year"
)

select "Title","Price"
from 
	"Game" as g
	inner join "Review" as r on g."Title" = r."GameTitle" and g."Year" = r."GameYear"
group by g."Title",g."Year"
having avg(r."Score") >= all game_review

	



