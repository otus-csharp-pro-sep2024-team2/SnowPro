using LessonService.Commands.Requests.Queries;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Commands.Queries.Handlers;

public class GetLessonStudentsQueryHandler(
    AppDbContext context,
    ILessonServiceApp lessonServiceApp,
    IServiceLogger logger
    ) : IRequestHandler<GetAllStudentsOfLessonQuery, ApiResponse<List<StudentModel>>>
{
    public async Task<ApiResponse<List<StudentModel>>> Handle(GetAllStudentsOfLessonQuery query, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<List<StudentModel>>();
        try
        {
            var lesson = await lessonServiceApp.FindLesson(query.LessonId, cancellationToken);
            response.Data = await context.LessonGroups
                .Where(lg => lg.LessonId == query.LessonId)
                .Select(p => new StudentModel(p.StudentId, p.Student.Name))
                .ToListAsync(cancellationToken: cancellationToken);
            response.Message = "Students list has been loaded.";
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error getting lesson: {ex.Message}.");
            throw;
        }        
    }
}