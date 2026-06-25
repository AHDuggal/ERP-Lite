using ERPLite.API.Middleware;

namespace ERPLite.API.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(
        this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();

        app.UseMiddleware<GlobalExceptionMiddleware>();

        return app;
    }
}