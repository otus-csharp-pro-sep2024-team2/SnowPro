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
/*
{
    public LessonResponse() 
    {
        Id = Guid.Empty;
        Name = "";
        Description = "";
        DateFrom = DateTime.Now;
        Duration = 0;
        MaxStudents = 0;
        LessonType = LessonType.None;
        TrainingLevel = TrainingLevel.Beginner;
        LessonStatus = LessonStatus.Scheduled;
        Instructor = new InstructorModel(Guid.Empty, "");
        Students = new List<StudentResponse>();
    }
}
*/