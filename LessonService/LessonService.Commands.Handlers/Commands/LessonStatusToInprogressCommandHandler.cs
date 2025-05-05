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

public class LessonStatusToInprogressCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<LessonStatusToInprogressCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(LessonStatusToInprogressCommand command, CancellationToken cancellationToken)
    {
        return await unitOfWork.Lessons.SetLessonStatus(command.LessonId,
            LessonStatus.InProgress,
            cancellationToken);
    }
}