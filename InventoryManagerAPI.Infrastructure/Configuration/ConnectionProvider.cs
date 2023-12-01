using Microsoft.Extensions.Configuration;
using IConnectionProvider = InventoryManagerAPI.Domain.Configuration.IConfigurationManager;

namespace InventoryManagerAPI.Infrastructure.Configuration;

public class ConnectionProvider : IConnectionProvider
{
    private readonly IConfiguration _configuration;

    public ConnectionProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? GetConnectionString()
    {
        return _configuration.GetConnectionString("DefaultConnection");
    }
}