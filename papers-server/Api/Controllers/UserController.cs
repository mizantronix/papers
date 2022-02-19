﻿using Papers.Api.Models;
using Papers.Common.Exceptions;

namespace Papers.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Papers.Common.Enums;
    using Papers.Common.Helpers;
    using Papers.Domain.Managers;
    using Papers.Domain.Models.User;

    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserManager userManager;

        public UserController(ILogger<WeatherForecastController> logger, IUserManager userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public SendResult Register([FromBody]RegisterDataModel registerData)
        {
            if (registerData == null)
            {
                throw new PapersBusinessException($"{nameof(registerData)} is empty");
            }

            this.userManager.Register(
                new UserInfo
                {
                    Login = registerData.Login,
                    FirstName = registerData.FirstName,
                    LastName = registerData.LastName,
                    UserPhone = registerData.Phone
                },
                registerData.Password);
            return SendResult.Success;
        }

        [HttpPost]
        [Route("confirm")]
        public SendResult Confirm(string phone, string code)
        {
            this.userManager.ConfirmUser(phone, code);
            return SendResult.Success;
        }

        [HttpGet]
        [Route("test")]
        public string TestGenerator(long id, string phone, string login)
        {
            return CommonExtensions.GenerateConfirmCode(id, phone, login);
        }
    }
}