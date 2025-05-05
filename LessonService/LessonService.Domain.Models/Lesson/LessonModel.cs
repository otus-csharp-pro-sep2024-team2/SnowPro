namespace LessonService.Domain.Models.Lesson;

public record LessonModel(
    Guid Id,
    string Name,
    string Description,
    DateTime DateFrom,
    int Duration,
    int MaxStudents,
    LessonTypeModel LessonType,
    TrainingLevelModel TrainingLevel,
    LessonStatusModel LessonStatus,
    InstructorModel? Instructor,
    List<StudentModel> Students
);
