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

public class UnEnrollStudentCommandHandler(
    IUnitOfWork unitOfWork,
    IServiceLogger logger,
    IMapper mapper) : IRequestHandler<UnEnrollStudentCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(UnEnrollStudentCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await unitOfWork.Lessons.FindLesson(command.LessonId, cancellationToken);
            var student = await unitOfWork.Lessons.FindStudent(command.StudentId, cancellationToken);
           lesson.UnEnrollStudent(student);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResponse<LessonModel>
            {
                Message = $"Student '{student.Name}' was unenrolled.",
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