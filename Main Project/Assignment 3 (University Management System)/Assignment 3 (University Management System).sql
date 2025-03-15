CREATE TABLE Students (
    StudentID SERIAL PRIMARY KEY,
    StudentName VARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender VARCHAR(10) CHECK (Gender IN ('Male', 'Female', 'Other')),
    EnrollmentDate DATE NOT NULL,
    Major VARCHAR(100) NOT NULL
);

CREATE TABLE Courses (
    CourseID SERIAL PRIMARY KEY,
    CourseName VARCHAR(100) NOT NULL,
    Department VARCHAR(100) NOT NULL,
    Credits INT NOT NULL
);

CREATE TABLE Professors (
    ProfessorID SERIAL PRIMARY KEY,
    ProfessorName VARCHAR(100) NOT NULL,
    Department VARCHAR(100) NOT NULL
);

CREATE TABLE Enrollments (
    EnrollmentID SERIAL PRIMARY KEY,
    StudentID INT REFERENCES Students(StudentID),
    CourseID INT REFERENCES Courses(CourseID),
    EnrollmentDate DATE NOT NULL
);

CREATE TABLE Grades (
    GradeID SERIAL PRIMARY KEY,
    StudentID INT REFERENCES Students(StudentID),
    CourseID INT REFERENCES Courses(CourseID),
    Grade VARCHAR(2) CHECK (Grade IN ('A', 'B', 'C', 'D', 'F')),
    Semester VARCHAR(10) NOT NULL
);

CREATE TABLE Departments (
    DepartmentID SERIAL PRIMARY KEY,
    DepartmentName VARCHAR(100) NOT NULL,
    Dean VARCHAR(100) NOT NULL
);

CREATE TABLE Attendance (
    AttendanceID SERIAL PRIMARY KEY,
    StudentID INT REFERENCES Students(StudentID),
    CourseID INT REFERENCES Courses(CourseID),
    Date DATE NOT NULL,
    Status VARCHAR(10) CHECK (Status IN ('Present', 'Absent'))
);

INSERT INTO Students (StudentName, DateOfBirth, Gender, EnrollmentDate, Major) VALUES
('abcde pqrst', '2003-05-15', 'Male', '2021-08-01', 'Computer Science'),
('bcdea qrstp', '2003-07-21', 'Female', '2021-09-01', 'Mathematics'),
('cdeab rstpq', '2003-02-10', 'Male', '2021-08-15', 'Physics'),
('deabc stpqr', '2003-11-03', 'Female', '2021-08-20', 'Biology'),
('eabcd tpqrs', '2003-04-18', 'Male', '2021-08-01', 'Chemistry');

INSERT INTO Courses (CourseName, Department, Credits) VALUES
('Database Systems', 'Computer Science', 3),
('Calculus I', 'Mathematics', 4),
('Physics 101', 'Physics', 3),
('Organic Chemistry', 'Chemistry', 4),
('Biology Basics', 'Biology', 3);

INSERT INTO Professors (ProfessorName, Department) VALUES
('Dr. Alice Brown', 'Mathematics'),
('Dr. John White', 'Computer Science'),
('Dr. Robert Green', 'Physics'),
('Dr. Emily Carter', 'Chemistry'),
('Dr. David Lee', 'Biology');

INSERT INTO Enrollments (StudentID, CourseID, EnrollmentDate) VALUES
(1, 1, '2024-01-10'),
(2, 2, '2024-01-11'),
(3, 3, '2024-01-12'),
(4, 4, '2024-01-13'),
(5, 5, '2024-01-14');

INSERT INTO Grades (StudentID, CourseID, Grade, Semester) VALUES
(1, 1, 'A', 'Fall 2023'),
(2, 2, 'B', 'Fall 2023'),
(3, 3, 'C', 'Fall 2023'),
(4, 4, 'D', 'Fall 2023'),
(5, 5, 'F', 'Fall 2023');

INSERT INTO Departments (DepartmentName, Dean) VALUES
('Computer Science', 'Dr. Emily Carter'),
('Mathematics', 'Dr. John White'),
('Physics', 'Dr. Robert Green'),
('Chemistry', 'Dr. Alice Brown'),
('Biology', 'Dr. David Lee');

INSERT INTO Attendance (StudentID, CourseID, Date, Status) VALUES
(1, 1, '2024-02-10', 'Present'),
(2, 2, '2024-02-11', 'Absent'),
(3, 3, '2024-02-12', 'Present'),
(4, 4, '2024-02-13', 'Present'),
(5, 5, '2024-02-14', 'Absent');

select * from Students;
select * from Courses;
select * from Professors;
select * from Enrollments;
select * from Grades;
select * from Departments;
select * from Attendance;



-- 1. Retrieve the average grade for each course.
-- • Calculate the average grade received by students in each course
SELECT CourseID, AVG(CASE Grade 
    WHEN 'A' THEN 4 WHEN 'B' THEN 3 WHEN 'C' THEN 2 WHEN 'D' THEN 1 ELSE 0 END) AS Avg_Grade
FROM Grades
GROUP BY CourseID
ORDER BY CourseID;

-- 2. Find the top 5 students with the highest GPA.
-- • Identify the students with the best overall performance based on their grades.
SELECT StudentID, AVG(CASE Grade 
    WHEN 'A' THEN 4 WHEN 'B' THEN 3 WHEN 'C' THEN 2 WHEN 'D' THEN 1 ELSE 0 END) AS GPA
FROM Grades
GROUP BY StudentID
ORDER BY GPA DESC
LIMIT 5;

-- 3. Count the number of students enrolled in each major.
-- • Determine how many students are pursuing each major.	
SELECT Major, COUNT(*) AS StudentCount
FROM Students
GROUP BY Major;

-- 4. Identify the courses with the highest student enrollment.
-- • Find out which courses are the most popular among students.
SELECT CourseID, COUNT(*) AS EnrolledStudents
FROM Enrollments
GROUP BY CourseID
ORDER BY EnrolledStudents DESC;

-- 5. Calculate the student retention rate.
-- • Determine the percentage of students who continue beyond their first year.
SELECT (COUNT(*) FILTER (WHERE EnrollmentDate >= CURRENT_DATE - INTERVAL '1 year') * 100.0 / COUNT(*)) AS RetentionRate
FROM Students;

-- 6. Find the professors teaching the most courses.
-- • Identify which professors are handling the highest number of courses.
SELECT Professors.ProfessorID, Professors.ProfessorName, COUNT(Courses.CourseID) AS CoursesTaught
FROM Professors
JOIN Courses ON Professors.Department = Courses.Department
GROUP BY Professors.ProfessorID
ORDER BY CoursesTaught DESC;

-- 7. List students who have failed more than one course.
-- • Identify students who received an "F" in multiple courses.SELECT StudentID, COUNT(*) AS FailedCourses
FROM Grades
WHERE Grade = 'F'
GROUP BY StudentID
HAVING COUNT(*) > 1;

-- 8. Analyze semester-wise student performance trends.
-- • Track changes in students' average grades across different semesters.
SELECT Semester, AVG(CASE Grade 
    WHEN 'A' THEN 4 WHEN 'B' THEN 3 WHEN 'C' THEN 2 WHEN 'D' THEN 1 ELSE 0 END) AS AvgGrade
FROM Grades
GROUP BY Semester
ORDER BY Semester;

-- 9. Calculate the percentage of students passing each course.
-- • Find out how many students received passing grades (A, B, or C) for each course.
SELECT CourseID, (COUNT(*) FILTER (WHERE Grade IN ('A', 'B', 'C')) * 100.0 / COUNT(*)) AS PassRate
FROM Grades
GROUP BY CourseID;

-- 10. Find students who changed their major after enrollment.
-- • Identify students who switched from one major to another.
select * from students;
SELECT StudentID, COUNT(DISTINCT Major) AS MajorChanges
FROM Students
GROUP BY StudentID
HAVING COUNT(DISTINCT Major) > 1;

-- 11. Determine the course completion rate.
-- • Calculate how many students completed each course versus those who dropped out.
SELECT CourseID, COUNT(*) FILTER (WHERE Grade IS NOT NULL) * 100.0 / COUNT(*) AS CompletionRate
FROM Enrollments
LEFT JOIN Grades ON Enrollments.StudentID = Grades.StudentID AND Enrollments.CourseID = Grades.CourseID
GROUP BY CourseID;


-- 12. Identify professors whose students have the highest average grades.
-- • Find professors whose students perform the best academically.
SELECT p.ProfessorID, p.ProfessorName, AVG(
    CASE g.Grade 
        WHEN 'A' THEN 4 WHEN 'B' THEN 3 WHEN 'C' THEN 2 WHEN 'D' THEN 1 ELSE 0 
    END) AS Avg_Grade
FROM Professors p
JOIN Courses c ON p.Department = c.Department
JOIN Grades g ON c.CourseID = g.CourseID
GROUP BY p.ProfessorID
ORDER BY Avg_Grade DESC;

-- 13. Calculate the attendance rate for each student.
-- • Find out how often each student attends their enrolled courses.
SELECT a.StudentID, s.StudentName, 
       COUNT(CASE WHEN a.Status = 'Present' THEN 1 END) * 100.0 / COUNT(*) AS AttendanceRate
FROM Attendance a
JOIN Students s ON a.StudentID = s.StudentID
GROUP BY a.StudentID, s.StudentName;

-- 14. Identify the most frequently skipped courses.
-- • Determine which courses have the highest absentee rates.
SELECT c.CourseID, c.CourseName, 
       COUNT(CASE WHEN a.Status = 'Absent' THEN 1 END) AS Absences
FROM Attendance a
JOIN Courses c ON a.CourseID = c.CourseID
GROUP BY c.CourseID, c.CourseName
ORDER BY Absences DESC;

-- 15. Find the department with the highest student enrollment.
-- • Identify which university department has the most students.
SELECT s.Major AS Department, COUNT(*) AS StudentCount
FROM Students s
GROUP BY s.Major
ORDER BY StudentCount DESC
LIMIT 1;



