using Microsoft.AspNetCore.Http;

namespace InventoryManagerAPI.Domain.File;

public interface IFileUploader
{
    public Task<Tuple<bool, List<string>>> UploadFilesAsync(List<IFormFile> files, HttpContext context);
}