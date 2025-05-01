using AuthorizationService.Domain.Models;
using AuthorizationService.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        SeedInitialData(context);
    }
    private static void SeedInitialData(ApplicationDbContext context)
    {
        if (context.Roles.Any()) return;
        context.Roles.AddRange(
            new Role { Id=1, Name = "Admin" },
            new Role { Id=2, Name = "Client" },
            new Role { Id=3, Name = "Instructor" }
        );
        context.SaveChanges();
    }    
}