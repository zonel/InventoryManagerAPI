using InventoryManagerAPI.Domain.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManager.Application.Handlers;

public class FileHandlerFactory : IFileHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public FileHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IFileHandler GetFileHandler(string filePath)
    {
        if (filePath.Contains("Inventory.csv"))
        {
            return _serviceProvider.GetRequiredService<InventoryFileHandler>();
        }
        else if (filePath.Contains("Products.csv"))
        {
            return _serviceProvider.GetRequiredService<ProductFileHandler>();
        }
        else if (filePath.Contains("Prices.csv"))
        {
            return _serviceProvider.GetRequiredService<PriceFileHandler>();
        }
        else
        {
            throw new NotSupportedException($"No handler found for file: {filePath}");
        }
    }
}