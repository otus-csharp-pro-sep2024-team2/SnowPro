namespace LessonService.Domain.Entities.Base.Exceptions;

public class StudentNotEnrolledException(Student student): Exception($"Student '{student.FirstName} {student.LastName}' is not enrolled in the lesson");