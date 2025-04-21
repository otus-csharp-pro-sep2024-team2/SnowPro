using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Interfaces;
using LessonService.Domain.ValueObjects;

namespace LessonService.Domain.Entities;

public class Student(Guid id) : Person(id), IStudent
{
    public Student() : this(Guid.Empty)
    {
    }
    public IEnumerable<LessonGroup>? LessonGroups { get; set; }
    
}