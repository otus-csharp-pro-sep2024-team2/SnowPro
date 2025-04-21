﻿using AutoMapper;
using LessonService.Application.Services.Messages;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LessonService.Application.Services;

public class LessonServiceApp(
    AppDbContext context,
    ILogger<LessonServiceApp> logger,
    IMapper mapper,
    IMessageService messageSender) : ILessonServiceApp
{
    public async Task<Lesson> FindLesson(Guid lessonId,CancellationToken cancellationToken)
    {
        var lesson = await context.Lessons
            .Include(l => l.Instructor)
            .Include(l => l.LessonGroups)
            .ThenInclude(lg => lg.Student)
            .FirstOrDefaultAsync(l => l.Id == lessonId, cancellationToken: cancellationToken);            

        if (lesson == null)
        {
            throw new LessonIsNotFoundException(lessonId);
        }
        return lesson;
    }
    public async Task<LessonGroup?> FindGroup(Guid lessonId, Guid studentId)
    {
        var group = await context.LessonGroups
            .Include(g => g.Student)
            .FirstOrDefaultAsync(x => x.LessonId == lessonId && x.Student.Id == studentId);
        if (group == null)
        {
            logger.LogError($"Student with ID: {studentId} not found.");
            throw new StudentIsNotFoundException(studentId);
        }
        return group;
    }

    public async Task<ApiResponse<LessonModel>> SetLessonStatus(Guid lessonId, LessonStatus lessonStatus, CancellationToken cancellationToken)
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
    private async Task SendNotification(Lesson lesson)
    {
        foreach(var student in lesson.GetStudents())
        {
            await messageSender.PublishNotification(lesson, student, NotificationType.Cancellation);
        }
        if(lesson.Instructor != null)
            await messageSender.PublishNotification(lesson, lesson.Instructor, NotificationType.Cancellation);
    }
    
}