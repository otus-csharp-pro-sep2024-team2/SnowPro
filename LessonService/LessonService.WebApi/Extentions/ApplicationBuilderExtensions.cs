using LessonService.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.WebApi.Extentions;

public static class ApplicationBuilderExtensions
{
    static LogWrapper _logWrapper;

    public static void UseEfMigration(this WebApplication application)
    {
        using var databaseContextScope = application.Services.CreateScope();
        var database = databaseContextScope.ServiceProvider.GetRequiredService<AppDbContext>();
        database.Database.Migrate();
    }

    public static void UseLogWrapper(this IApplicationBuilder app)
    {
        if (_logWrapper != null)
        {
            throw new InvalidOperationException("The logger already exists.");
        }
        var domainLogger = app.ApplicationServices.GetRequiredService<IServiceLogger>();
        _logWrapper = new LogWrapper(Log.Logger, domainLogger);
    } 
}