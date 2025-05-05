using AutoMapper;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Interfaces;
using MediatR;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Commands.Commands;

public class CreateLessonCommandHandler(
    IUnitOfWork unitOfWork,
    IServiceLogger logger,
    IMapper mapper) : IRequestHandler<CreateLessonCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(CreateLessonCommand lessonInfo, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = mapper.Map<CreateLessonCommand, Lesson>(lessonInfo);
            await unitOfWork.Lessons.AddAsync(lesson, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResponse<LessonModel>
            {
                Message = "Lesson has been created.",
                Data = mapper.Map<LessonModel>(lesson)
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create lesson");
            throw;
        }
    }
}