using AutoMapper;
using LessonService.Commands.Requests.Queries;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Interfaces;
using MediatR;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Commands.Queries;

public class GetLessonByIdQueryHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IServiceLogger logger
    ) : IRequestHandler<GetLessonByIdQuery, ApiResponse<LessonModel>>
{
    public async Task<ApiResponse<LessonModel>> Handle(GetLessonByIdQuery query, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<LessonModel>();
        try
        {
            var lesson = await unitOfWork.Lessons.FindLesson(query.LessonId, cancellationToken);
            response.Data = mapper.Map<LessonModel>(lesson);
            response.Message = "Lesson has been loaded successfully";
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error getting lesson: {ex.Message}");
            throw;
        }        
    }
}