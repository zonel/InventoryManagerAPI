using InventoryManagerAPI.Domain.Filtering;

namespace InventoryManager.Application.Filters;

public class Filtering : IFilterEntity
{
    public Task<List<T>> FilterEntitiesAsync<T>(List<T> entities, Func<T, bool> predicate)
    {
        return Task.Run(() => entities.Where(predicate).ToList());
    }
}