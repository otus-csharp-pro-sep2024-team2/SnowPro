using AutoMapper;
using LessonService.Application.Services.Messages;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands;

public class RemoveInstructorCommandHandler(AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    IMessageService messageSender,
    ILogger<RemoveInstructorCommandHandler> logger,
    IMapper mapper): IRequestHandler<RemoveInstructorCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(RemoveInstructorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            lesson.RemoveInstructor();
            await context.SaveChangesAsync(cancellationToken);
            if (lesson.Instructor != null)
            {
                await messageSender.PublishNotification(lesson, lesson.Instructor, NotificationType.InstructorUnassigned);
            }
            var response = new ApiResponse<LessonModel>
            {
                Message = "Instructor was removed.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            logger.LogInformation(response.Message);
            return response;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error assigning instructor: {ex.Message}");
            throw;
        }
    }
}