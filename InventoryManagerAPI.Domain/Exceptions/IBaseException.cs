namespace InventoryManagerAPI.Domain.Exceptions;

public interface IBaseException
{
    int statusCode { get; }
}