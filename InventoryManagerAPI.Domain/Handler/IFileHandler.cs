namespace InventoryManagerAPI.Domain.Handler;

public interface IFileHandler
{
    Task HandleAsync(List<string> fileContent);
}