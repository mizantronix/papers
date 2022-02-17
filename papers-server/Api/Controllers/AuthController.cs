using Microsoft.AspNetCore.Http;
using Papers.Api.Attributes;

namespace Papers.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    using Papers.Api.Authentication;
    using Papers.Common.Enums;
    using Papers.Common.Exceptions;
    using Papers.Common.Helpers;
    using Papers.Domain.Managers;

    [ApiController]
    [Route("")]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IHttpContextAccessor _context;

        public AuthController(IUserManager userManager, IHttpContextAccessor context)
        {
            this._userManager = userManager;
            this._context = context;
        }

        [HttpGet]
        [Access(UserState.New)]
        [Route("whoami")]
        public IActionResult WhoAmI() 
        {
            var name = User.Identity?.Name;
            return Ok($"{name}");
        }


        [HttpPost]
        [Route("token")]
        public IActionResult Token(string identifier, string password)
        {
            var user = this._userManager.GetByIdentifier(identifier);
            if (user == null)
            {
                return BadRequest(new { error = $"user with identifier {identifier} nor found" });
            }

            if (user.PasswordHash != password.GetPasswordHash())
            {
                return BadRequest(new { error = "login or password is invalid " });
            }

            var claimList = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, user.UserInfo.Login),
                new (ClaimsIdentity.DefaultRoleClaimType, user.State.ToString())
            };
            
            var claimsIdentity = new ClaimsIdentity(claimList, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.Now;
            var expires = now.Add(TimeSpan.FromMinutes(AuthOptions.TokenLifetime));
            var jst = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jst);

            var response = new
            {
                access_token = encodedJwt,
                expires = expires
            };

            User.AddIdentity(claimsIdentity);

            return Ok(response);
        }
    }
}
