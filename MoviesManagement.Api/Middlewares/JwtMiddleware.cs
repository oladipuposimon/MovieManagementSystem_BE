using Microsoft.Extensions.Options;
using MoviesManagement.Domain.Helpers;
using MoviesManagement.Services.Services.Interfaces;

namespace MoviesManagement.Api.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            string? userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                Guid user_Id = Guid.Parse(userId);
                context.Items["User"] = await userService.GetUserAsync((user_Id));
            }

            await _next(context);
        }
    }
}
