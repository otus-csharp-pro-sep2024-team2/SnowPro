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

public class DeleteLessonCommandHandler(
    IUnitOfWork unitOfWork,
    IServiceLogger logger,
    IMapper mapper) : IRequestHandler<DeleteLessonCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(DeleteLessonCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await unitOfWork.Lessons.GetLessonByIdAsync(command.LessonId, cancellationToken);
            await unitOfWork.Lessons.RemoveAsync(lesson);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResponse<LessonModel>
            {
                Message = "Lesson was deleted.",
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