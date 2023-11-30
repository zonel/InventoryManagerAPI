using InventoryManagerAPI.Domain.Filtering;

namespace InventoryManager.Application.Filters;

public class Filtering : IFilterEntity
{
    public List<T> FilterEntities<T>(List<T> entities, Func<T, bool> predicate)
    {
        return entities.Where(predicate).ToList();
    }
}