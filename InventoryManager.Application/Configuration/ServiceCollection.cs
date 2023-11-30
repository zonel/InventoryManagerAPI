using System.Reflection;
using InventoryManager.Application.File;
using InventoryManager.Application.Filters;
using InventoryManager.Application.Handlers;
using InventoryManager.Application.Mapping;
using InventoryManager.Application.Mapping.EntityMappings;
using InventoryManager.Application.Mapping.MappingConfigurations;
using InventoryManagerAPI.Domain.File;
using InventoryManagerAPI.Domain.Filtering;
using InventoryManagerAPI.Domain.Handler;
using InventoryManagerAPI.Domain.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManager.Application.Configuration;


public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFileSaver, Save>();
        services.AddScoped<IFileValidator, Validate>();
        services.AddScoped<IFileUploader, Upload>();
        services.AddScoped<ICsvMapper, MapCsv>();
        services.AddScoped<IFileHandlerFactory, FileHandlerFactory>();
        services.AddScoped<InventoryFileHandler>();
        services.AddScoped<ProductFileHandler>();
        services.AddScoped<PriceFileHandler>();
        services.AddScoped<IMappingConfigurationsFactory, MappingConfigurationsFactory>();

        services.AddScoped<InventoryClassMap>();
        services.AddScoped<PriceClassMap>();
        services.AddScoped<ProductClassMap>();
        
        services.AddScoped<IFilterEntity, Filtering>();
        
        return services;
    }
}