using AutoMapper;
using LessonService.Commands.Requests.Queries;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Commands.Queries.Handlers;

public class GetLessonsQueryHandler(
    AppDbContext context,
    IMapper mapper,
    IServiceLogger logger
    ) : IRequestHandler<GetAllLessonsQuery, ApiResponse<IEnumerable<LessonModel>>>
{
    public async Task<ApiResponse<IEnumerable<LessonModel>>> Handle(GetAllLessonsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var response = new ApiResponse<IEnumerable<LessonModel>>();

            var lessons = await context.Lessons
                .Where(lesson => true)
                .Include(lesson => lesson.Instructor)
                .Include(lesson => lesson.LessonGroups)
                .ThenInclude(lg => lg.Student)
                .ToListAsync(cancellationToken: cancellationToken);
            response.Data = mapper.Map<IEnumerable<LessonModel>>(lessons);
            response.Message = "Lessons has been loaded successfully";
            logger.LogInformation(response.Message);
            return response;
            
        }
        catch (Exception e)
        {
            logger.LogError($"Error getting list of lessons: {e.Message}");
            throw;
        }        
    }
}