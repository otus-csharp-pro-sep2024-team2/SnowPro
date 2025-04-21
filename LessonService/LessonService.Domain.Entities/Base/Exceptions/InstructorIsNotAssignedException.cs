namespace LessonService.Domain.Entities.Base.Exceptions;

public class InstructorIsNotAssignedException(Lesson lesson) : Exception($"The lesson '{lesson.Name}' does not have any instructor assigned.");
