using System.Reflection;
using InventoryManager.Application.File;
using InventoryManagerAPI.Domain.File;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManager.Application.Configuration;


public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFileSaver, Save>();
        services.AddScoped<IFileValidator, Validate>();
        services.AddScoped<IFileUploader, Upload>();
        return services;
    }
}