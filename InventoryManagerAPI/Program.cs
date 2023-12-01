using System.Reflection;
using InventoryManager.Application.Configuration;
using InventoryManagerAPI.Configuration;
using InventoryManagerAPI.Infrastructure.Configuration;
using InventoryManagerAPI.Middleware;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//swagger annotations
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(s => s.IncludeXmlComments(xmlPath))
    .AddControllers();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
});

var app = builder.Build();

#region HTMLpipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion