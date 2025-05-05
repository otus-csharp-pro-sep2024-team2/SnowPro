using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;

namespace LessonService.Interfaces;

public interface ILessonRepository
{
    Task<Lesson?> FindLesson(Guid id, CancellationToken cancellationToken);
    Task<Instructor?> FindInstructor(Guid id, CancellationToken cancellationToken);
    Task<Student?> FindStudent(Guid id, CancellationToken cancellationToken);
    
    Task<Lesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Lesson>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Lesson lesson, CancellationToken cancellationToken);
    Task RemoveAsync(Lesson lesson);
    void Update(Lesson lesson);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<Person?> GetPersonByIdAsync<T>(Guid id, CancellationToken cancellationToken)  where T : Person;
    Task<ApiResponse<LessonModel>> SetLessonStatus(Guid lessonId, LessonStatus lessonStatus, CancellationToken cancellationToken);
    
}