using AutoMapper;
using LessonService.Application.Services.Messages;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Interfaces;
using MediatR;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Commands.Commands;

public class AssignInstructorCommandHandler(
    IUnitOfWork unitOfWork,
    IMessageService messageSender,
    IServiceLogger logger,
    IMapper mapper) : IRequestHandler<AssignInstructorCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(AssignInstructorCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await unitOfWork.Lessons.FindLesson(command.LessonId, cancellationToken);
            var instructor = await unitOfWork.Lessons.FindInstructor(command.InstructorId, cancellationToken);
            lesson.AssignInstructor(instructor);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await messageSender.PublishNotification(lesson, instructor, NotificationType.InstructorAssigned);

            return new ApiResponse<LessonModel>
            {
                Message = "Instructor assigned successfully",
                Data = mapper.Map<LessonModel>(lesson)
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to assign instructor");
            throw;
        }
    }
}