using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
});

var app = builder.Build();

#region Endpoints
// Endpoint 1: Data Import
app.MapPost("/importData", async context =>
{
    var uploadFileSizeLimitInMb = 500;
    var formOptions = new FormOptions { MultipartBodyLengthLimit = uploadFileSizeLimitInMb * 1024 * 1024 }; // 500 MB limit
    var form = await context.Request.ReadFormAsync(formOptions);
    var files = form.Files.ToList();

    if(files == null || files.Count != 3)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync("Please provide all three CSV files.");
    }

    foreach (var file in files)
    {
        if (file.ContentType != "text/csv")
        {
            context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
            await context.Response.WriteAsync("Only CSV files are allowed.");
            return;
        }

        if (file.Length > 500 * 1024 * 1024)
        {
            context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
            await context.Response.WriteAsync("File size exceeds the limit (500 MB).");
            return;
        }

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);
        //if Uploads folder doesn't exist, create it
        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")))
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"));
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    await context.Response.WriteAsync($"Files uploaded successfully.");
});

// Endpoint 2: Product Information Retrieval
app.MapGet("/products/{SKU}", async (HttpContext context, string sku) =>
{
    await context.Response.WriteAsync($"Product information for SKU: {sku}");
});
#endregion

#region HTMLpipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion