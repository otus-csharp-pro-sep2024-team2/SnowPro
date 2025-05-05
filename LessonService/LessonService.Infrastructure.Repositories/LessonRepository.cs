using AutoMapper;
using LessonService.Application.Services.Messages;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using Microsoft.EntityFrameworkCore;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Infrastructure.Repositories;

public class LessonRepository(
    AppDbContext context,
    IMessageService messageSender,
    IServiceLogger logger,
    IMapper mapper
) : ILessonRepository
{
    public async Task<Lesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Lessons
            .Where(lesson => true)
            .Include(lesson => lesson.Instructor)
            .Include(lesson => lesson.LessonGroups)
            .ThenInclude(lg => lg.Student).FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<List<Lesson>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Lessons.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Lesson lesson, CancellationToken cancellationToken)
    {
        await context.Lessons.AddAsync(lesson, cancellationToken);
    }

    public Task RemoveAsync(Lesson lesson)
    {
        context.Lessons.Remove(lesson);
        return Task.CompletedTask;
    }

    public void Update(Lesson lesson)
    {
        context.Lessons.Update(lesson);
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Person?> GetPersonByIdAsync<T>(Guid id, CancellationToken cancellationToken)  where T : Person
    {
        return await context.Persons
            .OfType<T>() 
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
 public async Task<Lesson> FindLesson(Guid lessonId,CancellationToken cancellationToken)
 {
     var lesson = await GetByIdAsync(lessonId, cancellationToken);
        if (lesson == null)
        {
            throw new LessonIsNotFoundException(lessonId);
        }
        return lesson;
    }

    public async Task<Instructor> FindInstructor(Guid id, CancellationToken cancellationToken)
    {
        var person = await GetPersonByIdAsync<Instructor>(id, cancellationToken);
        if (person == null)
        {
            throw new InstructorIsNotFoundException(id);
        }
        return (Instructor)person;
    }
    public async Task<Student> FindStudent(Guid id, CancellationToken cancellationToken)
    {
        var person = await GetPersonByIdAsync<Student>(id, cancellationToken);
        if (person == null)
        {
            throw new StudentIsNotFoundException(id);
        }
        return (Student)person;
    }
    
    private async Task SendNotification(Lesson lesson)
    {
        foreach(var student in lesson.GetStudents())
        {
            await messageSender.PublishNotification(lesson, student, NotificationType.Cancellation);
        }
        if(lesson.Instructor != null)
            await messageSender.PublishNotification(lesson, lesson.Instructor, NotificationType.Cancellation);
    }
    public async Task<ApiResponse<LessonModel>> SetLessonStatus(Guid lessonId,
        LessonStatus lessonStatus,
        CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await FindLesson(lessonId, cancellationToken);
            lesson.SetStatus(lessonStatus);
            await context.SaveChangesAsync(cancellationToken);
            
            var response = new ApiResponse<LessonModel>
            {
                Message = "Lesson's status was changed.",
                Data = mapper.Map<LessonModel>(lesson)
            };
            logger.LogInformation(response.Message);
            if (lessonStatus == LessonStatus.Cancelled)
            {
                await SendNotification(lesson);
            }
            return response;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error changing lesson's status: {ex.Message}");
            throw;
        }
    }
        
}