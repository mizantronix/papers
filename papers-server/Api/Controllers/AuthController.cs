﻿namespace Papers.Api.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Text;
    
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    using Papers.Api.Authentication;
    using Papers.Api.Attributes;
    using Papers.Common.Enums;
    using Papers.Common.Helpers;
    using Papers.Domain.Managers;

    [ApiController]
    [Route("")]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public AuthController(IUserManager userManager)
        {
            this._userManager = userManager;
        }

        [HttpGet]
        [Access(UserState.Registered, UserState.Removed, UserState.New)]
        [Route("authTest")]
        public IActionResult Test()
        {
            return Ok();
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


            var handler = new JwtSecurityTokenHandler();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.SecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var identity = 
                new ClaimsIdentity(new GenericIdentity(user.UserInfo.Login),
                new[]
                {
                    new Claim("user_id", user.Id.ToString()),
                    new Claim("user_state", user.State.ToString()),

                    // TODO auth source
                    // new Claim("auth_source", user.State.ToString())
                });

            var token = handler.CreateJwtSecurityToken(subject: identity,
                signingCredentials: signingCredentials,
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                expires: DateTime.UtcNow.AddSeconds(42));
            return Ok(handler.WriteToken(token));
        }
    }
}
