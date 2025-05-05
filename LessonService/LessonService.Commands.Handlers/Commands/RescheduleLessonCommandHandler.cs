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

public class RescheduleLessonCommandHandler(
    IUnitOfWork unitOfWork,
    IServiceLogger logger,
    IMapper mapper) : IRequestHandler<RescheduleLessonCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(RescheduleLessonCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await unitOfWork.Lessons.GetLessonByIdAsync(command.LessonId, cancellationToken);
            lesson.Reschedule(DateTime.SpecifyKind(command.DateFrom, DateTimeKind.Utc), command.Duration);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new ApiResponse<LessonModel>
            {
                Message = "Lesson was rescheduled.",
                Data = mapper.Map<LessonModel>(lesson)
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw;
        }
    }
}