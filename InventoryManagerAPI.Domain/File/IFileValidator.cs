using Microsoft.AspNetCore.Http;

namespace InventoryManagerAPI.Domain.File;

public interface IFileValidator
{
    bool ValidateFiles(List<IFormFile> files, HttpContext context);
}