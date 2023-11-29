using Microsoft.AspNetCore.Http;

namespace InventoryManagerAPI.Domain.File;

public interface IFileSaver
{
    public Task<string> SaveFileAsync(IFormFile file);
}