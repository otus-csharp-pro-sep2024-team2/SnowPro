using AutoMapper;
using LessonService.Application.Services.Messages;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands;

public class RescheduleLessonCommandHandler(
    AppDbContext context, 
    IMessageService messageSender,
    IMapper mapper, 
    ILessonServiceApp lessonServiceApp,
    ILogger<RescheduleLessonCommand> logger) : IRequestHandler<RescheduleLessonCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(RescheduleLessonCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            lesson.Reschedule(DateTime.SpecifyKind(command.DateFrom, DateTimeKind.Utc), command.Duration);
            await context.SaveChangesAsync(cancellationToken);
            await SendNotification(lesson);
            var response = new ApiResponse<LessonModel>
            {
                Message = "Lesson was rescheduled.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            logger.LogInformation(response.Message);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw;
        }
    }
    private async Task SendNotification(Lesson lesson)
    {
        foreach(var student in lesson.GetStudents())
        {
            await messageSender.PublishNotification(lesson, student, NotificationType.Reschedule);
        }
        if(lesson.Instructor != null)
            await messageSender.PublishNotification(lesson, lesson.Instructor, NotificationType.Reschedule);
    }
}