namespace Papers.Api.Attributes
{
    using System;

    using Microsoft.AspNetCore.Authorization;

    using Papers.Common.Enums;

    [AttributeUsage(AttributeTargets.Method)]
    public class AccessAttribute : AuthorizeAttribute
    {
        public UserState UserState;

        private AccessAttribute()
        {
        }

        public AccessAttribute(UserState userState)
        {
            this.UserState = userState;
        }
    }
}
