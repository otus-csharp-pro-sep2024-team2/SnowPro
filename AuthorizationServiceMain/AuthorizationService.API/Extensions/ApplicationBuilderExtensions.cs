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
        var rolesToAdd = new List<Role>
        {
            new() { Id = 1, Name = "Admin" },
            new() { Id = 2, Name = "Client" },
            new() { Id = 3, Name = "Instructor" }
        };

        foreach (var role in rolesToAdd.Where(role => !context.Roles.Any(r => r.Name == role.Name)))
        {
            context.Roles.Add(role);
        }

        context.SaveChanges();
    }    
}