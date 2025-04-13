using CargoWatch.Application.Interfaces;
using CargoWatch.Domain.Entities;
using CargoWatch.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CargoWatch.Infrastructure;

public static class DataLayerConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)//, IConfiguration configuration)
    {
        //var connectionString = configuration.GetConnectionString("DefaultConnection")
        //    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        var connectionString = "Data Source=cargowatch.db";

        services.AddDbContext<CargoWatchDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
            .AddEntityFrameworkStores<CargoWatchDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddScoped<IAppDbContext>(provider =>
            provider.GetRequiredService<CargoWatchDbContext>());

        return services;
    }
}
