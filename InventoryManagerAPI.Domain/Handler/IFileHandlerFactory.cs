namespace InventoryManagerAPI.Domain.Handler;

public interface IFileHandlerFactory
{
    public IFileHandler GetFileHandler(string filePath);
}