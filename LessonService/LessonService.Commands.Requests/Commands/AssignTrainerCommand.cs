using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Requests.Commands;

public record AssignInstructorCommand(Guid LessonId, Guid InstructorId, string InstructorName):  IRequest<ApiResponse<LessonModel>>;