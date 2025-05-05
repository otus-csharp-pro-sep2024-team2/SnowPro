using AutoMapper;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Interfaces;
using MediatR;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Commands.Commands;

public class EnrollStudentCommandHandler(
    IUnitOfWork unitOfWork,
    IServiceLogger logger,
    IMapper mapper) : IRequestHandler<EnrollStudentCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(EnrollStudentCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await unitOfWork.Lessons.GetLessonByIdAsync(command.LessonId, cancellationToken);
            var student = await unitOfWork.Lessons.GetStudentByIdAsync(command.StudentId, cancellationToken);
            lesson.EnrollStudent(student);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new ApiResponse<LessonModel>
            {
                Message = $"Student '{student.Name}' was enrolled.",
                Data = mapper.Map<LessonModel>(lesson)
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Handler failed");
            throw;
        }
    }
}