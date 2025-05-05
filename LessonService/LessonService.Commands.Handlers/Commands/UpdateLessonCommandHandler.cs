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

public class UpdateLessonCommandHandler(
    IUnitOfWork unitOfWork,
    IServiceLogger logger,
    IMapper mapper) : IRequestHandler<UpdateLessonCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(UpdateLessonCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await unitOfWork.Lessons.GetLessonByIdAsync(command.LessonId, cancellationToken);

            if (command.MaxStudents.HasValue) lesson.SetMaxStudents(command.MaxStudents.Value);
            if (command.Name is not null) lesson.SetName(command.Name);
            if (command.Description is not null) lesson.SetDescription(command.Description);
            if (command.LessonType.HasValue) lesson.SetLessonType((LessonType)command.LessonType);
            if (command.TrainingLevel.HasValue) lesson.SetTrainingLevel((TrainingLevel)command.TrainingLevel);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResponse<LessonModel>
            {
                Message = "Lesson was updated.",
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