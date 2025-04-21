using AutoMapper;
using LessonService.Application.Services.Messages;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Domain.ValueObjects;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands;


public class AssignInstructorCommandHandler(
    AppDbContext context,
    IMessageService messageSender,
    ILessonServiceApp lessonServiceApp,
    ILogger<AssignInstructorCommandHandler> logger,
    IMapper mapper) : IRequestHandler<AssignInstructorCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(AssignInstructorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            var instructor = await context.Persons.OfType<Instructor>().FirstOrDefaultAsync(s => s.UserId == command.InstructorId);
            if (instructor == null)
            {
                throw new InstructorIsNotFoundException(command.InstructorId);
            }
            lesson.AssignInstructor(instructor);
            await context.SaveChangesAsync(cancellationToken);
            await messageSender.PublishNotification(lesson, instructor, NotificationType.InstructorAssigned);

            var response = new ApiResponse<LessonModel>
            {
                Message = "Instructor was assigned.",
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