using AutoMapper;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using MediatR;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Commands.Commands;

public class CreateLessonCommandHandler(
    AppDbContext context,
    IServiceLogger logger,
    IMapper mapper) : IRequestHandler<CreateLessonCommand, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(CreateLessonCommand lessonInfo, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = mapper.Map<CreateLessonCommand, Lesson>(lessonInfo);
            context.Lessons.Add(lesson);
            await context.SaveChangesAsync(cancellationToken);
            var response = new ApiResponse<LessonModel>
            {
                Message = "Lesson has been created.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            logger.LogInformation(response.Message);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error creating lesson: {ex.Message}");
            throw;
        }
    }
}