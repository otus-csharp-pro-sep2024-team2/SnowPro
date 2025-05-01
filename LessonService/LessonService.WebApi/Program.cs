using LessonService.WebApi.Endpoints;
using LessonService.WebApi.Extentions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Read configuration for Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.UseLogWrapper();

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
app.UseExceptionHandler();

app.MapGroup("/api/v1")
    .WithTags("LessonService endpoints")
    .MapLessonEndPoint();

// Apply migrations
app.UseEfMigration();



app.Run();
