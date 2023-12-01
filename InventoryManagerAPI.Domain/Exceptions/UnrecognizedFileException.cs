namespace InventoryManagerAPI.Domain.Exceptions;

public class UnrecognizedFileException : Exception, IBaseException
{
    private readonly int _statusCode = 404;
    public int statusCode => _statusCode;
    public UnrecognizedFileException(string message) : base(message)
    {
        
    }
}
