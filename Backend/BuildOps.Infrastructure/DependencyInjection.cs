using System;
using System.Collections.Generic;
using System.Text;


using BuildOps.Application.Interfaces;
using BuildOps.Infrastructure.Persistence;
using BuildOps.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildOps.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Database connection string is missing.");

        services.AddDbContext<BuildOpsDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<
            IEquipmentCategoryRepository,
            EquipmentCategoryRepository>();

        return services;
    }
}