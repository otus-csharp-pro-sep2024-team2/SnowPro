using LessonService.Infrastructure.EF;
using LessonService.WebApi.Endpoints;
using LessonService.WebApi.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
       c.SwaggerEndpoint("/swagger/v1/swagger.json", "SnowPro LessonService API v1");
    });
}
// Autentification

    app.UseAuthentication();
    app.UseAuthorization();



app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapGroup("/api/v1")
    .WithTags("LessonService endpoints")
    .MapLessonEndPoint();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // Apply migrations
}

app.Run();
