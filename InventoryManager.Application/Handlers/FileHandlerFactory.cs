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
        IFileHandler fileHandler;
    
        switch (filePath)
        {
            case var _ when filePath.Contains("Inventory.csv"):
                fileHandler = _serviceProvider.GetRequiredService<InventoryFileHandler>();
                break;
            case var _ when filePath.Contains("Products.csv"):
                fileHandler = _serviceProvider.GetRequiredService<ProductFileHandler>();
                break;
            case var _ when filePath.Contains("Prices.csv"):
                fileHandler = _serviceProvider.GetRequiredService<PriceFileHandler>();
                break;
            default:
                throw new NotSupportedException($"No handler found for file: {filePath}");
        }
        return fileHandler;
    }
}