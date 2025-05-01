using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Interfaces;
using MediatR;

namespace LessonService.Commands.Commands;


public class ChangeLessonStatusCommandHandler(
    ILessonServiceApp lessonServiceApp) : IRequestHandler<ChangeLessonStatusCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(ChangeLessonStatusCommand command,
        CancellationToken cancellationToken)
    {
        return await lessonServiceApp.SetLessonStatus(command.LessonId, (LessonStatus)command.LessonStatus, cancellationToken);
    }
}