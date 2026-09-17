// SportsStore.WebUI/Middleware/ExceptionHandlerMiddleware.cs
using System.Net;
using System.Text.Json;

namespace SportsStore.WebUI.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Chỉ áp dụng global exception handler cho các API request (/api/...)
        // Các request MVC thông thường vẫn dùng exception handler mặc định của ASP.NET Core
        if (!context.Request.Path.StartsWithSegments("/api"))
        {
            await _next(context);
            return;
        }

        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
