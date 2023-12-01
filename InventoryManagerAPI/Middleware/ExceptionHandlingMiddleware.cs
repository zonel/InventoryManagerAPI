namespace InventoryManagerAPI.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            string errorMessage = e switch
            {
                BadHttpRequestException badHttpRequest => $"[{badHttpRequest.StatusCode}] {badHttpRequest.GetType().Name} - {badHttpRequest.Message}",
                _ => "[Middleware] An unexpected error occurred."
            };

            await context.Response.WriteAsync(errorMessage);
        }
    }
}