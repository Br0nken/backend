using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AccessControlApp.Middleware
{
    public class RoleBasedRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleBasedRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<IdentityUser> userManager, ILogger<RoleBasedRedirectMiddleware> logger)
        {
            if (context.Request.Path.StartsWithSegments("/Admin"))
            {
                var user = await userManager.GetUserAsync(context.User);
                if (user == null || !await userManager.IsInRoleAsync(user, "Admin"))
                {
                    logger.LogWarning($"Access denied to {context.Request.Path} for user {user?.UserName}");
                    context.Response.Redirect("/Home/AccessDenied");
                    return;
                }
            }
            await _next(context);
        }
    }
}