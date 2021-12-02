﻿namespace Papers.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Papers.Common.Enums;
    using Papers.Domain.Managers;

    [ApiController]
    [Route("messages")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMessageManager messageManager;

        public MessageController(ILogger<WeatherForecastController> logger, IMessageManager messageManager)
        {
            _logger = logger;
            this.messageManager = messageManager;
        }

        [HttpPost]
        public SendResult Send(long chatId )
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