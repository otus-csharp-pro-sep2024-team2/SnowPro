using AutoMapper;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Enums;
using MassTransit;
using SnowPro.Shared.Contracts;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Application.Services.Messages;

public class MessageService(
    IPublishEndpoint publishEndpoint,
    IServiceLogger logger,
    IMapper mapper)
    : IMessageService
{
  
    public async Task Publish<T>(T message)
    {
        try
        {
            await publishEndpoint.Publish(message);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while publishing message");
        }
    }

    public async Task PublishNotification(Lesson lesson, Person person, NotificationType notificationType)
    {
        var message = mapper.Map<NotificationMessageDto>(person);
        message.Message = notificationType switch
        {
            NotificationType.Enrollment => GetEnrollmentMessage(lesson, (Student)person),
            NotificationType.UnEnrollment => GetUnEnrollmentMessage(lesson, (Student)person),
            NotificationType.Reschedule => GetLessonRescheduledMessage(lesson),
            NotificationType.Cancellation => GetLessonCancelledMessage(lesson),
            NotificationType.Reminder => $"Reminder: The lesson '{lesson.Name}' will start soon.",
            NotificationType.InstructorAssigned => GetInstructorAssignedMessage(lesson),
            NotificationType.InstructorUnassigned => GetInstructorUnassignedMessage(lesson),
            _ => throw new ArgumentOutOfRangeException(nameof(notificationType), notificationType, null)
        };

        string[] messageType = ["email", "sms", "telegram"]; 
        foreach (var msgType in messageType)
        {
            message.Type = msgType;
            await Publish(message);
            logger.LogInformation($"{notificationType} notification message published to {message.Username} by {msgType}.");
        }
    }
    private string GetEnrollmentMessage(Lesson lesson, Student student)
    {
        return   
$@"Dear Student {student.Name},
You are enrolled in the lesson {lesson.Name}.
The lesson will start at {lesson.DateFrom:dd.MM.yyyy HH:mm}.
The duration of the lesson is {lesson.Duration} minutes.";
    }

    private string GetUnEnrollmentMessage(Lesson lesson, Student student)
    {
        return  $@"Dear Student {student.Name},
You have been removed from the lesson {lesson.Name}.
The lesson date is {lesson.DateFrom:dd.MM.yyyy HH:mm}.";
    }
    private string GetLessonRescheduledMessage(Lesson lesson)
    {
        return $@"The lesson '{lesson.Name}' has been rescheduled.
The new lesson date is {lesson.DateFrom:dd.MM.yyyy HH:mm}.";
    }
    private string GetLessonCancelledMessage(Lesson lesson)
    {
        return $"The lesson {lesson.Name} was cancelled.";
    }
    private string GetInstructorAssignedMessage(Lesson lesson)
    {
        return $"You have been assigned as an instructor for the lesson '{lesson.Name}'.";
    }
    private string GetInstructorUnassignedMessage(Lesson lesson)
    {
        return $"You have been unassigned as an instructor for the lesson '{lesson.Name}'.";
    }
    
}