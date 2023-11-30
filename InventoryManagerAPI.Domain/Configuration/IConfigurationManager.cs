namespace InventoryManagerAPI.Domain.Configuration;

public interface IConfigurationManager
{
    public string? GetConnectionString();
}