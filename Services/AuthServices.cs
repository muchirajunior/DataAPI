using System.Security.Claims;

namespace DataAPI.Services;

public class JwtValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserService _userService;

    public JwtValidationMiddleware(RequestDelegate next, IUserService userService)
    {
        _next = next;
        _userService = userService;
    }

    public async Task Invoke(HttpContext context)
    {
        // Check if the user is authenticated
        if (context.User.Identity.IsAuthenticated)
        {
            // Get the user's ID from the token
            var userId = context.User.FindFirst(ClaimTypes.Email)?.Value;

            // Fetch user information from the user service
            var user = _userService.FetchUser(userId!);

            // Check if the user exists and if the token version matches
            if (user != null && user.SecurityStamp == context.User.FindFirst("jti")?.Value)
            {
                // Token is valid, proceed with the request
                await _next(context);
                return;
            }
        }

        // Token is invalid or user not found, return unauthorized
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
    }
}

public static class JwtValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtValidationMiddleware>();
    }
}
