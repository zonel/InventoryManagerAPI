namespace InventoryManagerAPI.Domain.Filtering;

public interface IFilterEntity
{
    public Task<List<T>> FilterEntitiesAsync<T>(List<T> entities, Func<T, bool> predicate);
}