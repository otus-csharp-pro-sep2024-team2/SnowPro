using LessonService.Commands.Requests.Queries;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Commands.Queries;

public class GetLessonStudentsQueryHandler(
    IUnitOfWork unitOfWork,
    IServiceLogger logger
    ) : IRequestHandler<GetAllStudentsOfLessonQuery, ApiResponse<List<StudentModel>>>
{
    public async Task<ApiResponse<List<StudentModel>>> Handle(GetAllStudentsOfLessonQuery query, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<List<StudentModel>>();
        try
        {
            response.Data = await unitOfWork.Lessons.GetLessonStudentsAsync(query.LessonId, cancellationToken); 
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