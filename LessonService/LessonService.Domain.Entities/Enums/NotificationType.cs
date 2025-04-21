namespace LessonService.Domain.Entities.Enums;

public enum NotificationType
{
    Reschedule = 1,
    Cancellation = 2,
    Reminder = 3,
    Enrollment = 4,
    UnEnrollment = 5,
    LessonStarted = 6,
    InstructorAssigned = 7,
    InstructorUnassigned = 8,
}