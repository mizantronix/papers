using Papers.Data.MsSql.Models;

namespace Papers.Api.Attributes
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Papers.Common.Enums;

    // TODO async
    [AttributeUsage(AttributeTargets.Method)]
    public class AccessAttribute : Attribute, IAuthorizationFilter
    {
        private const string HeaderName = "Authorization";

        public UserState? UserState;

        private AccessAttribute()
        {
        }

        // TODO use something else instead UserState
        public AccessAttribute(UserState? userState)
        {
            this.UserState = userState;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                var authCode = context.HttpContext.Request.Headers[HeaderName];
                
                if (this.UserState.HasValue && this.UserState != Common.Enums.UserState.New && string.IsNullOrEmpty(authCode))
                {
                    context.Result = new UnauthorizedObjectResult($"{HeaderName} is required");
                    return;
                }

                // TODO token validation
                if (false)
                {
                    context.Result = new UnauthorizedObjectResult($"{HeaderName} is invalid");
                    return;
                }
            }
        }
    }
}
