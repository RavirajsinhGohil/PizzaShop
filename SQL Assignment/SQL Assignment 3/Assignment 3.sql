-- Query 1
-- write a SQL query to find Employees who have the biggest salary in their Department
SELECT  D.Dept_Name ,MAX(E.Salary) AS Max_Salary FROM Employee E JOIN Department D ON E.Dept_ID = D.Dept_ID GROUP BY D.Dept_ID, D.Dept_Name;

-- Query 2
-- write a SQL query to find Departments that have less than 3 people in it
SELECT D.Dept_Name, COUNT(D.Dept_ID) AS NO_OF_EMPLOYEES FROM Department D JOIN Employee E ON D.Dept_ID = E.Dept_ID GROUP BY D.Dept_ID, D.Dept_Name HAVING COUNT(D.Dept_ID) < 3;

-- Query 3
-- write a SQL query to find All Department along with the number of people there
SELECT D.Dept_Name ,COUNT(D.Dept_ID) AS NO_OF_EMPLOYEES FROM Department D JOIN Employee E ON D.Dept_ID = E.Dept_ID GROUP BY D.Dept_ID, D.Dept_Name;

-- Query 4
-- write a SQL query to find All Department along with the total salary there
SELECT SUM(Salary) AS Each_Depat_Salary FROM Employee E GROUP BY Dept_ID;