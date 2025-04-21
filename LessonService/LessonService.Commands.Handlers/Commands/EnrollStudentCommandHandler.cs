using AutoMapper;
using LessonService.Application.Services.Messages;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Entities.EventMessages;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Domain.ValueObjects;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands;

public class EnrollStudentCommandHandler(
    AppDbContext context,
    IMapper mapper,
    IMessageService messageSender,
    ILessonServiceApp lessonServiceApp,
    ILogger<EnrollStudentCommand> logger) : IRequestHandler<EnrollStudentCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(EnrollStudentCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await lessonServiceApp.FindLesson(command.LessonId, cancellationToken);
            var student = await context.Persons.OfType<Student>().FirstOrDefaultAsync(s=>s.UserId == command.StudentId);
            if (student == null)
            {
                throw new StudentIsNotFoundException(command.StudentId);
            }
            var group = lesson.LessonGroups.FirstOrDefault(p => p.StudentId == command.StudentId);
            if (group != null)
            {
                throw new StudentIsEnrolledAlreadyException(lesson, student);
            }            
            
            lesson.EnrollStudent(student);
            await context.SaveChangesAsync(cancellationToken);
     
            var response = new ApiResponse<LessonModel>
            {
                Message = $"Student '{student.Name}' was enrolled.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            logger.LogInformation(response.Message);
            await messageSender.PublishNotification(lesson, student, NotificationType.Enrollment);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error enrolling student: {ex.Message}");
            throw;
        }
    }
}