using AutoMapper;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Domain.Entities.Enums;
using LessonService.Domain.Models.Lesson;
using SnowPro.Shared.Contracts;

namespace LessonService.Application.Services.Mapping;

public class LessonMapping: Profile
{
    public LessonMapping()
    {
        CreateMap<CreateLessonCommand, Lesson>()
            .ForCtorParam("id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("description", opt => opt.MapFrom(src => src.Description))
            .ForCtorParam("dateFrom", opt => opt.MapFrom(src => 
                DateTime.SpecifyKind(src.DateLesson, DateTimeKind.Utc) + src.TimeStart))
            .ForCtorParam("duration", opt => opt.MapFrom(src => src.Duration))
            .ForCtorParam("trainingLevel", opt => opt.MapFrom(src => src.TrainingLevel))
            .ForCtorParam("lessonType", opt => opt.MapFrom(src => src.LessonType))
            .ForCtorParam("maxStudents", opt => opt.MapFrom(src => src.MaxStudents));

        CreateMap<Lesson, CreateLessonCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DateLesson, opt => opt.MapFrom(src => src.DateFrom.Date))
            .ForMember(dest => dest.TimeStart, opt => opt.MapFrom(src => src.DateFrom.TimeOfDay))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
            .ForMember(dest => dest.TrainingLevel, opt => opt.MapFrom(src => src.TrainingLevel))
            .ForMember(dest => dest.LessonType, opt => opt.MapFrom(src => src.LessonType))
            .ForMember(dest => dest.MaxStudents, opt => opt.MapFrom(src => src.MaxStudents));
        
        
        CreateMap<Lesson, LessonModel>()
            .ConstructUsing(src => new LessonModel(
                src.Id,
                src.Name,
                src.Description,
                src.DateFrom,
                src.Duration,
                src.MaxStudents,
                new LessonTypeModel((int)src.LessonType, src.LessonType.ToString()),
                new TrainingLevelModel((int)src.TrainingLevel, src.TrainingLevel.ToString()),
                new LessonStatusModel((int)src.LessonStatus, src.LessonStatus.ToString()),
                src.Instructor != null ? new InstructorModel(src.Instructor.UserId, src.Instructor.Name.ToString()) : null,
                src.LessonGroups.Select(lg => new StudentModel(lg.Student.UserId, lg.Student.Name.ToString())).ToList()
            ));

        CreateMap<Instructor, InstructorModel>()
            .ConstructUsing(src => new InstructorModel(src.UserId, src.Name.ToString()));

        CreateMap<Student, StudentModel>()
            .ConstructUsing(src => new StudentModel(src.UserId, src.Name.ToString()));
        
        CreateMap<LessonStatus, LessonStatusModel>()
            .ConstructUsing(src => new LessonStatusModel((int)src, src.ToString()));        

        CreateMap<LessonType, LessonTypeModel>()
            .ConstructUsing(src => new LessonTypeModel((int)src, src.ToString()));        
        CreateMap<TrainingLevel, TrainingLevelModel>()
            .ConstructUsing(src => new TrainingLevelModel((int)src, src.ToString()));
        
        CreateMap<UserRegisteredDto, Person>()
            .ConstructUsing(src => new Person(src.UserId));
        
        CreateMap<UserRegisteredDto, Student>()
            .ConstructUsing(src => new Student(src.UserId));

        CreateMap<UserRegisteredDto, Instructor>()
            .ConstructUsing(src => new Instructor(src.UserId));
        
        CreateMap<Person, NotificationMessageDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.TelegramId));
        CreateMap<Student, NotificationMessageDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.TelegramId));
        CreateMap<Instructor,NotificationMessageDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.TelegramId));
            

    }
}