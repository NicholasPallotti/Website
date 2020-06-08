Select *
from Student
	inner join StudentClass on Student.StudentId = StudentClass.StudentId
	inner join Class on StudentClass.ClassId = Class.ClassId
order by Name