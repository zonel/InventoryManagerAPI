using Microsoft.Extensions.Configuration;
using IConfigurationManager = InventoryManagerAPI.Domain.Configuration.IConfigurationManager;

namespace InventoryManagerAPI.Infrastructure.Configuration;

public class ConfigurationManager : IConfigurationManager
{
    private readonly IConfiguration _configuration;

    public ConfigurationManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? GetConnectionString()
    {
        return _configuration.GetConnectionString("DefaultConnection");
    }
}