using CsvHelper;
using InventoryManagerAPI.Domain.Csv;
using InventoryManagerAPI.Infrastructure.Csv;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagerAPI.Infrastructure.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICsvFileReader, Read>();
        return services;
    }
}