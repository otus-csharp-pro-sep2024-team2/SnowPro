﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProfileService.Infrastructure.EntityFramework;

public static class EntityFrameworkInstaller
{
    public static IServiceCollection ConfigureContext(this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<DatabaseContext>(optionsBuilder
            => optionsBuilder
                .UseLazyLoadingProxies() // lazy loading
                .UseNpgsql(connectionString));

        return services;
    }
}
