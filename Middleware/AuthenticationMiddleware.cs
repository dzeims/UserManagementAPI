using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace UserManagementAPI.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip authentication for Swagger UI
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        // Validate token (demo only - use JWT in production)
        if (!context.Request.Headers.TryGetValue("Authorization", out var token) ||
            token != "Bearer valid-token")
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new { error = "Unauthorized" })
            );
            return;
        }

        await _next(context);
    }
}