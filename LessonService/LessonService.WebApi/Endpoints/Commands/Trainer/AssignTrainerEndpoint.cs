using LessonService.Commands.Requests.Commands;
using MediatR;

namespace LessonService.WebApi.Endpoints.Commands.Instructor;

public static class AssignInstructorEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to assign a instructor to the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPost($"{HelperEndpoint.baseUrl}/instructor",
                async (AssignInstructorCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return result.Data is not null? Results.Created($"{HelperEndpoint.baseUrl}/{result.Data.Id}", result) : Results.NotFound();
                }), "Assign a instructor to the lesson", "Endpoint to assign a instructor to the lesson")
            .WithName("AssignInstructor");
    }
}