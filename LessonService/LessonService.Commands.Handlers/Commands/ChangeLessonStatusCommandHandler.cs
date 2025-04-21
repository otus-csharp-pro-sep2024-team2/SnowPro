using AutoMapper;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands.Commands;


public class ChangeLessonStatusCommandHandler(
    AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    ILogger<ChangeLessonStatusCommandHandler> logger,
    IMapper mapper) : IRequestHandler<ChangeLessonStatusCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(ChangeLessonStatusCommand command,
        CancellationToken cancellationToken)
    {
        return await lessonServiceApp.SetLessonStatus(command.LessonId, (LessonStatus)command.LessonStatus, cancellationToken);
    }
}