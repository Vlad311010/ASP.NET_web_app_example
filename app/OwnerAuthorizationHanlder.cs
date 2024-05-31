using app.Utils;
using Microsoft.AspNetCore.Authorization;

namespace app.Authorization
{
    public class OwnerRequirement : IAuthorizationRequirement { }

    public class OwnerAuthorizationHanlder : AuthorizationHandler<OwnerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                string userLogin = context.User.GetLogin();
                if (userLogin == null)
                {
                    return Task.CompletedTask;
                }

                string resourceOwner = httpContext.Request.RouteValues["userLogin"]?.ToString();
                resourceOwner = resourceOwner ?? httpContext.User.GetLogin();
                if (resourceOwner == null)
                {
                    return Task.CompletedTask;
                }

                if (context.User.GetRole() == "Admin" || resourceOwner == userLogin)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

}
