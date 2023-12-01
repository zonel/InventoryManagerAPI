namespace InventoryManagerAPI.Domain.Exceptions;

public class UnaccessibleFileException : Exception, IBaseException
{
    private readonly int _statusCode = 404;
    public int statusCode => _statusCode;
    public UnaccessibleFileException(string message) : base(message)
    {
        
    }
}