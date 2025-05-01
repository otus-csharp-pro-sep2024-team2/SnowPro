using LessonService.WebApi.Endpoints.Commands.Lesson;
using LessonService.WebApi.Endpoints.Commands.Student;
using LessonService.WebApi.Endpoints.Commands.Instructor;
using LessonService.WebApi.Endpoints.Queries;

namespace LessonService.WebApi.Endpoints;

public static class LessonEndpoints
{
    /// <summary>
    /// LessonService Endpoints
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapLessonEndPoint(this IEndpointRouteBuilder app)
    {
        CreateLessonEndpoint.Map(app);
        GetAllLessonsEndpoint.Map(app);
        DeleteLessonEndpoint.Map(app);
        GetLessonByIdEndpoint.Map(app);
        AssignInstructorEndpoint.Map(app);
        RemoveInstructorEndpoint.Map(app);
        EnrollStudentEndpoint.Map(app);
        UnEnrollStudentEndpoint.Map(app);
        GetAllStudentsOfLessonEndpoint.Map(app);
        UpdateLessonEndpoint.Map(app);
        RescheduleLessonEndpoint.Map(app);
        LessonStatusToInprogressEndpoint.Map(app);
        LessonStatusToCompletedEndpoint.Map(app);
        LessonStatusToCancelledEndpoint.Map(app);
        return app;
    }
}