namespace Papers.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Papers.Api.Models;
    using System.Collections.Generic;
    using Papers.Common.Enums;
    using Papers.Common.Exceptions;
    using Papers.Domain.Managers;
    using Papers.Domain.Models.Message;

    [ApiController]
    [Route("messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageManager _messageManager;

        public MessageController(IMessageManager messageManager)
        {
            this._messageManager = messageManager;
        }

        [HttpPost]
        [Route("send")]
        public SendResult Send(SendMessageDataModel dataModel)
        {
            if (dataModel == null)
            {
                throw new PapersBusinessException("data model is null");
            }

            this._messageManager.Send(dataModel.Message.SenderId, dataModel.ChatId, dataModel.Message);
            return SendResult.Success;
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<Message> Get(long chatId, int from, int count)
        {
            var messages = this._messageManager.Get(chatId, from, count);
            return messages;
        }
    }
}