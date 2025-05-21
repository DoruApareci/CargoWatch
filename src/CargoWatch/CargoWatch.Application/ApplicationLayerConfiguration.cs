using CargoWatch.Application.Implementations;
using CargoWatch.Application.Interfaces;
using CargoWatch.Application.Models;
using CargoWatch.Domain.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CargoWatch.Application;
public static class ApplicationLayerConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.Configure<SendGridSettings>(config.GetSection(SendGridSettings.OptionKey));
        services.AddScoped<IEmailService, EmailService>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<DeviceMappingProfile>();
            cfg.AddProfile<DataSetMappingProfile>();
        });

        return services;
    }
}
