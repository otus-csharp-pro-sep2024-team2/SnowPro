using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Interfaces;
using LessonService.Domain.ValueObjects;

namespace LessonService.Domain.Entities;

public class Instructor(Guid id) : Person(id), IInstructor
{
    public Instructor() : this(Guid.Empty)
    {
    }
    public IEnumerable <Lesson>? Lessons { get; set; }    
}