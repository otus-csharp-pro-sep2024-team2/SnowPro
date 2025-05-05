﻿using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Base.Exceptions;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Entities.Interfaces;

namespace LessonService.Domain.Entities
{
    public class Lesson : Entity<Guid>, ILesson
    {
        public Lesson(Guid id,
            string name,
            string description,
            DateTime dateFrom,
            int duration,
            TrainingLevel trainingLevel, 
            LessonType lessonType, int maxStudents) : base (id)
        {
            Name = name;
            Description = description;
            DateFrom = dateFrom;
            Duration = duration;
            TrainingLevel = trainingLevel;
            LessonType = lessonType;
            LessonStatus = LessonStatus.Scheduled;
            MaxStudents = maxStudents;
            Instructor = null;
        }

        public Instructor? Instructor { get; private set; } //= new();

        public Guid? InstructorId { get; set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime DateFrom { get; private set; }
        public int Duration { get; private set; }
        public TrainingLevel TrainingLevel { get; private set; }
        public LessonType LessonType { get; private set; }
        public int MaxStudents { get; private set; }

        public LessonStatus LessonStatus { get; private set; } = LessonStatus.Scheduled;
        public ICollection<LessonGroup> LessonGroups { get; init; } = new List<LessonGroup>();  

        public string GetName() => Name;
        public string GetDescription() => Description;
        public DateTime GetDateFrom() => DateFrom;
        public int GetDuration() => Duration;
        public TrainingLevel GetTrainingLevel() => TrainingLevel;
        public LessonType GetLessonType() => LessonType;
        public int GetMaxStudents() => MaxStudents;
        public LessonStatus GetLessonStatus() => LessonStatus;
        public void SetName(string value) => Name = value;
        public void SetDescription(string value) => Description = value;
        public void SetTrainingLevel(TrainingLevel value) => TrainingLevel = value;
        public void SetLessonType(LessonType value) => LessonType = value;
        public void SetMaxStudents(int value) => MaxStudents = value;
        public void ValidateLesson()
        {
            if (LessonStatus == 0)
                LessonStatus = LessonStatus.Scheduled;
            
            if (LessonStatus == LessonStatus.Scheduled)
                return;
            
            throw LessonStatus switch
            {
                LessonStatus.Completed => new LessonAlreadyCompletedException(this),
                LessonStatus.InProgress => new LessonInProgressException(this),
                LessonStatus.Cancelled => new LessonCancelledException(this),
                _ => new LessonStatusNotValidException(this)
            };
        }
        public void EnrollStudent(Student student)
        {
            if (LessonGroups.Count >= MaxStudents)                     
                throw new LessonMaxStudentException(this);
            ValidateLesson();
            LessonGroups.Add(new LessonGroup() { Lesson = this, Student = (Student)student });
        }

        public void UnEnrollStudent(Student student)
        {
            ValidateLesson();
            var group = LessonGroups.FirstOrDefault(p => p.LessonId == this.Id && p.StudentId == (student).Id); 
            if (group == null)
                throw new StudentNotEnrolledException(student);
            LessonGroups.Remove(group);
        }

        public IEnumerable<Student> GetStudents()
        {
            return LessonGroups.Select(p => p.Student).ToList();
        }

        public void Reschedule(DateTime dateFromValue, int durationValue)
        {
            if (DateFrom == dateFromValue && Duration == durationValue)
                throw new RescheduleLessonDateTimeException(this);
            if (durationValue <= 1)
                throw new RescheduleLessonDurationException(this);
            DateFrom = dateFromValue;
            Duration = durationValue;
        }

        public void CancelLesson()
        {
            if (LessonStatus == LessonStatus.Cancelled)
                throw new LessonCancelledException(this);
            if (LessonStatus == LessonStatus.Completed)
                throw new LessonCompletedException(this);
            if (LessonStatus == LessonStatus.InProgress)
                throw new LessonInPorgressException(this);
            
            LessonStatus = LessonStatus.Cancelled;
        }

        public void CompleteLesson()
        {
            if (LessonStatus != LessonStatus.InProgress)
            {
                throw LessonStatus switch
                {
                    LessonStatus.Scheduled => new CompletedNotStartedException(this),
                    LessonStatus.Completed => new CompletedAlreadyComletedException(this),
                    LessonStatus.Cancelled => new CompletedIsCancelledException(this),
                };
            }
            LessonStatus = LessonStatus.Completed;
        }
        public void StartLesson()
        {
            ValidateLesson();
            if (Instructor == null)
                throw new InstructorIsNotAssignedException(this);
            if (LessonGroups.Count == 0)
                throw new LessonHasNoErolledStudent(this);
            LessonStatus = LessonStatus.InProgress;
        }

        public void AssignInstructor(Instructor instructor)
        {
            ValidateLesson();
            Instructor = (Instructor) instructor;
        }
        public void RemoveInstructor()
        {
            if (Instructor != null)
                ValidateLesson();
            Instructor = null;
        }
        public Lesson() : base(Guid.NewGuid())
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Description = string.Empty;
        }
        public Lesson(string name, string description ) : base(Guid.NewGuid())
        {
            Name = name;
            Description = description;
        }

        public void SetStatus(LessonStatus lessonStatus)
        {
            switch (lessonStatus)
            {
                case LessonStatus.Scheduled:
                    throw new LessonCannotBeScheduledException(this);
                case LessonStatus.Cancelled:
                    CancelLesson();
                    break;
                case LessonStatus.Completed:
                    CompleteLesson();
                    break;
                case LessonStatus.InProgress:
                    StartLesson();
                    break;
                default:
                    throw new InvalidLessonStatusValueException((int)lessonStatus);
            }
        }
    }
}
