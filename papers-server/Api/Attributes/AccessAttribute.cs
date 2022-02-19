namespace Papers.Api.Attributes
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Papers.Api.Authentication;
    using Papers.Common.Enums;

    // TODO async
    [AttributeUsage(AttributeTargets.Method)]
    public class AccessAttribute : Attribute, IAuthorizationFilter
    {
        private const string HeaderName = "Authorization";
        public UserState[] UserStates;

        private AccessAttribute()
        {
        }

        // TODO use something else instead UserStates (mb with [flag])
        public AccessAttribute(params UserState[] userStates)
        {
            this.UserStates = userStates;
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
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = AuthOptions.GetTokenValidationParams();

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var stateClaim = principal.Identities.First().Claims.FirstOrDefault(c => c.Type == "user_state");
                if (this.UserStates.Any(userState => stateClaim.Value == userState.ToString()))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}
