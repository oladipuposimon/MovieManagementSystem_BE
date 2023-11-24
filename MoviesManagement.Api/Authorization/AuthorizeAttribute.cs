using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviesManagement.Domain.Entities;

namespace MoviesManagement.Api.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute()
        {  
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // authorization
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
        }
    }
}
