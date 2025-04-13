using CargoWatch.Domain.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CargoWatch.Application;
public static class ApplicationLayerConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<DeviceMappingProfile>();
            cfg.AddProfile<DataSetMappingProfile>();
        });

        return services;
    }
}
