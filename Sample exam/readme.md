# Instructions
Given the empty .Net project, extend it according to the following assignments. Copy the code from the exam into this file as indicated. Only copy the relevant code. Failure to copy the right code will possibly result in not scoring the points of one question.

**Only deliver this file, and nothing else**

Grading of each assignment is all-or-nothing. An assignment does not grant partial points.

## Assignment 1 (2 points)
Setup the database schema and fill it with data. Create models for:
- Students (name, student code, birthday)
- Courses (name, description, study points, start date, end date)
- CompletedCourses (student, course, grade, date)

Fill the database with random data.

Copy here the code of the models and the DB context class:

```cs

```

Copy here the code of the creation of students, courses, and completed courses:

```cs

```

## Assignment 2 (2 points)
Write a LINQ query that extracts courses:
- the first 10
- ordered by start date
- where the study points are between 6 and 8

Copy here the code of the LINQ query written in any LINQ syntax you prefer:

```cs

```

## Assignment 3 (2 points)
Write a LINQ query that extracts all the courses completed for each student with a join. Only extract the following fields:
- student code
- course name
- course study points
- grade

Copy here the code of the LINQ query that performs the join:

```cs

```

## Assignment 4 (2 points)
Write a LINQ query that calculates the average grade and the total study points of each student through an aggregation.

Copy here the code of the LINQ query that performs the aggregation:

```cs

```

## Assignment 5 (2 points)
Write a LINQ query that finds all courses from this year. Using a subquery and a join, find all students who passed at least one course from this year.

Copy here the code of the LINQ query with the subquery:

```cs

```