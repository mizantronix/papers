using Papers.Common.Contract.Enums;
using Papers.Domain.Managers;

namespace Papers.Api.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

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
        public SendResult Register()
        {

            return SendResult.Success;
        }

        [HttpPost]
        public SendResult Send(long chatId)
        {
            this.messageManager.Send(1);

            return SendResult.Success;
        }

        [HttpGet]
        public string Test()
        {
            this.messageManager.Test();
            return "123";
        }
    }
}