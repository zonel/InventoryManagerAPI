using CsvHelper;
using InventoryManagerAPI.Domain.Configuration;
using InventoryManagerAPI.Domain.Csv;
using InventoryManagerAPI.Domain.DatabaseInteraction;
using InventoryManagerAPI.Domain.Repositories;
using InventoryManagerAPI.Infrastructure.Csv;
using InventoryManagerAPI.Infrastructure.DatabaseInteraction;
using InventoryManagerAPI.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagerAPI.Infrastructure.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICsvFileReader, Read>();
        services.AddScoped<IDatabaseBulkInsert, Insert>();
        services.AddScoped<IConfigurationManager, ConnectionProvider>();
        services.AddScoped<IDatabaseRepository, DatabaseRepository>();
        
        services.AddScoped<ICsvFileReader, Read>()
            .AddScoped<IDatabaseBulkInsert, Insert>()
            .AddScoped<IConfigurationManager, ConnectionProvider>();
        services.AddScoped<IDatabaseBulkInsert, Insert>();
        services.AddScoped<IConfigurationManager, ConnectionProvider>();
        services.AddScoped<IDatabaseRepository, DatabaseRepository>();
        return services;
    }
}