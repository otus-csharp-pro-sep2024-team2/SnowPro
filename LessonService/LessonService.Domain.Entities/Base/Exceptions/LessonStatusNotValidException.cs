namespace LessonService.Domain.Entities.Base.Exceptions;
public class LessonStatusNotValidException(Lesson lesson) : Exception($"Lesson '{lesson.Name}'  status is unknown");
