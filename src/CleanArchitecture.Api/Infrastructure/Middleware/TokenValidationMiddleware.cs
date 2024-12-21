using CleanArchitecture.Application.Logout.Services;
using CleanArchitecture.Infrastructure.Services;

namespace CleanArchitecture.Api.Infrastructure.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenValidationMiddleware> _logger;
        private readonly ITokenBlackListService _tokenBlackListService;
        public TokenValidationMiddleware(RequestDelegate next, ILogger<TokenValidationMiddleware> logger, ITokenBlackListService tokenBlackListService)
        {
            _next = next;
            _logger = logger;
            _tokenBlackListService = tokenBlackListService ?? throw new ArgumentNullException(nameof(tokenBlackListService));
        }
        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Processing request path: {Path}", context.Request.Path);
            _logger.LogInformation("Authorization header present: {AuthorizationPresent}", context.Request.Headers.ContainsKey("Authorization"));

            var excludedPaths = new[] { "/api/login", "/api/registeruser" };
            if (excludedPaths.Any(path => context.Request.Path.StartsWithSegments(path, StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);
                return;
            }

            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Headers["Authorization"].ToString().Split(" ").Last();
                if (!string.IsNullOrEmpty(token))
                {
                    if (_tokenBlackListService.IsTokenRevoked(token))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Unauthorized: Token has been revoked");
                        return;
                    }

                    await _next(context);
                    return;
                }
            }

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Token is missing");
        }
    }
}
