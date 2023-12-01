namespace InventoryManagerAPI.Domain.Repositories;

public interface IDatabaseRepository
{
    public Task<IEnumerable<T>> GetProductDataAsync<T>(string sku);
}