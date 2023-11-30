using CsvHelper;
using InventoryManagerAPI.Domain.Configuration;
using InventoryManagerAPI.Domain.Csv;
using InventoryManagerAPI.Domain.DatabaseInteraction;
using InventoryManagerAPI.Infrastructure.Csv;
using InventoryManagerAPI.Infrastructure.DatabaseInteraction;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagerAPI.Infrastructure.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICsvFileReader, Read>();
        services.AddScoped<IDatabaseBulkInsert, Insert>();
        services.AddScoped<IConfigurationManager, ConfigurationManager>();
        return services;
    }
}