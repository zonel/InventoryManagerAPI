namespace InventoryManagerAPI.Domain.Filtering;

public interface IFilterEntity
{
    public List<T> FilterEntities<T>(List<T> entities, Func<T, bool> predicate);
}