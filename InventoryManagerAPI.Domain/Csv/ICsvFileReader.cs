namespace InventoryManagerAPI.Domain.Csv;

public interface ICsvFileReader
{
    public Task<IEnumerable<string>> ReadFileAsync(string filePath);
}