namespace InventoryManagerAPI.Domain.Exceptions;

public class MappingException : Exception, IBaseException
{
    private readonly int _statusCode = 404;
    public int statusCode => _statusCode;
    public MappingException(string message) : base(message)
    {
        
    }
}