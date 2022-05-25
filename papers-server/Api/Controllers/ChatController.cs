namespace Papers.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Papers.Common.Enums;
    using Papers.Common.Exceptions;
    using Papers.Domain.Managers;
    using Papers.Domain.Models.User;

    [ApiController]
    [Route("chats")]
    public class ChatController : ControllerBase
    {
        private readonly IChatManager _chatManager;
        private readonly IUserManager _userManager;

        public ChatController(IChatManager chatManager, IUserManager userManager)
        {
            this._userManager = userManager;
            this._chatManager = chatManager;
        }

        [HttpPost]
        [Route("create")]
        public SendResult CreateChat(long creatorId, long targetUserId, bool isPrivate)
        {
            var creatorUser = this._userManager.GetById(creatorId);
            if (creatorUser == null)
            {
                throw new PapersBusinessException($"Creator user with id {creatorId} not found");
            }

            var targetUser = this._userManager.GetById(targetUserId);
            if (targetUser == null)
            {
                throw new PapersBusinessException($"Target user with id {targetUserId} not found");
            }

            this._chatManager.CreateSingleChat(creatorId, targetUserId, isPrivate);
            
            return SendResult.Success;
        }

        [HttpGet]
        [Route("{id}")]
        public SendResult GetChatById(long id)
        {
            var chat = this._chatManager.GetById(id);
            return SendResult.Success;
        }
    }
}
