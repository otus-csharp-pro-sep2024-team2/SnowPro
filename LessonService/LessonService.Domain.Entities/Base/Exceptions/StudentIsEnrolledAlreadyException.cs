﻿namespace LessonService.Domain.Entities.Base.Exceptions;

public class StudentIsEnrolledAlreadyException(Lesson lesson, Student student)
    : Exception($"Student '{student.FirstName} {student.LastName}' is already enrolled in the lesson '{lesson.Name}'.");