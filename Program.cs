using JsonColumnTest;
using Microsoft.EntityFrameworkCore;
using NodaTime;

using var ctx = new TestContext();
ctx.Database.Migrate();

ctx.Students.Add(new Student
{
    Name = "John Doe",
    ExamGrades = new List<ExamGrade>
    {
        new()
        {
            Grade = Grade.A,
            Subject = "Math",
            Date = new LocalDate(2021, 1, 1)
        },
        new()
        {
            Grade = Grade.B,
            Subject = "English",
            Date = new LocalDate(2021, 1, 2)
        }
    }
});
ctx.Students.Add(new Student
{
    Name = "Jane Doe",
    ExamGrades = new List<ExamGrade>
    {
        new()
        {
            Grade = Grade.A,
            Subject = "Computer Science",
            Date = new LocalDate(2022, 1, 1)
        }
    }
});

// NullRef Exception here
ctx.SaveChanges();

var threshold = new LocalDate(2021, 12, 10);
var student = ctx.Students
                 .FirstOrDefault(s => s.ExamGrades
                                       .Any(eg => eg.Date > threshold));

Console.WriteLine($"Student {student.Name} has {student.ExamGrades.Count} exam grades");