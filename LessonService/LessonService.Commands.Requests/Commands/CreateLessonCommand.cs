﻿using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Requests.Commands;

public record CreateLessonCommand(
    Guid Id,
    string Name,
    string Description,
    DateTime DateLesson,
    TimeSpan TimeStart,
    int Duration,
    TrainingLevel TrainingLevel, 
    LessonType LessonType, 
    int MaxStudents) : IRequest<ApiResponse<LessonModel>>;
