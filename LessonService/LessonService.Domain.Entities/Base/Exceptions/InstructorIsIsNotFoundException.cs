namespace LessonService.Domain.Entities.Base.Exceptions;

public class InstructorIsNotFoundException(Guid id) : Exception($"Instructor {id} is not found");