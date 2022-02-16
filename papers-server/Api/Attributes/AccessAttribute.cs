namespace Papers.Api.Attributes
{
    using System;
    using System.IdentityModel.Tokens.Jwt;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Papers.Api.Authentication;
    using Papers.Common.Enums;

    // TODO async
    [AttributeUsage(AttributeTargets.Method)]
    public class AccessAttribute : Attribute, IAuthorizationFilter
    {
        private const string HeaderName = "Authorization";
        public UserState UserState;

        private AccessAttribute()
        {
        }

        // TODO use something else instead UserState (mb with [flag])
        public AccessAttribute(UserState userState)
        {
            this.UserState = userState;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                var authCode = context.HttpContext.Request.Headers[HeaderName];
                
                if (string.IsNullOrEmpty(authCode))
                {
                    context.Result = new UnauthorizedObjectResult($"{HeaderName} is required");
                    return;
                }

                // TODO token validation
                if (!TokenIsValid(context.HttpContext.Request.Headers[HeaderName]))
                {
                    context.Result = new UnauthorizedObjectResult($"{HeaderName} is invalid");
                }
            }
        }

        private bool TokenIsValid(string token)
        {
            token = token.Replace("Bearer ", string.Empty);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = AuthOptions.GetTokenValidationParams();

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }
    }
}
