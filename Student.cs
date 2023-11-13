using NodaTime;

namespace JsonColumnTest;

public class Student
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required List<ExamGrade> ExamGrades { get; set; }
}

public sealed class ExamGrade
{
    public Grade Grade { get; set; }
    public required string Subject { get; set; }
    public LocalDate Date { get; set; }
}

public enum Grade
{
    A,
    B,
    C,
    D,
    F
}