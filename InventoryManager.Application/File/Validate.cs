using InventoryManagerAPI.Domain.File;
using Microsoft.AspNetCore.Http;

namespace InventoryManager.Application.File;

public class Validate : IFileValidator
{
    public bool ValidateFiles(List<IFormFile> files, HttpContext context)
    {
        if (files == null || files.Count != 3)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.WriteAsync("Please provide all three CSV files.");
            return false;
        }

        foreach (var file in files)
        {
            if (file.ContentType != "text/csv")
            {
                context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                context.Response.WriteAsync("Only CSV files are allowed.");
                return false;
            }

            if (file.Length > 500 * 1024 * 1024)
            {
                context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
                context.Response.WriteAsync("File size exceeds the limit (500 MB).");
                return false;
            }
        }
        return true;
    }
}