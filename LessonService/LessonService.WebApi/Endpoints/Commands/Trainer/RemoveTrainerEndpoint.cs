using LessonService.Commands.Requests.Commands;
using MediatR;

namespace LessonService.WebApi.Endpoints.Commands.Trainer;

public static class RemoveInstructorEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to remove a instructor from the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapDelete($"{HelperEndpoint.baseUrl}/instructor/{{lessonId:guid}}",
                async (Guid lessonId, IMediator mediator) =>
                {
                    var response = await mediator.Send(new RemoveInstructorCommand(lessonId));
                    return response;
                }), 
                "Remove a instructor from the lesson", "Endpoint to remove a instructor from the lesson")
            .WithName("RemoveInstructor");
    }
}