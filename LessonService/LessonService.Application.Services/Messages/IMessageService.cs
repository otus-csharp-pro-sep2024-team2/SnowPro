using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Enums;

namespace LessonService.Application.Services.Messages
{
    public interface IMessageService 
    {
        Task Publish<T>(T message);
        Task PublishNotification(Lesson lesson, Person student, NotificationType notificationType);
    }
}

