using Papers.Common.Exceptions;

namespace Papers.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Papers.Common.Enums;
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
        public SendResult CreateChat(long creatorId, long targetUserId)
        {
            var targetUser = this._userManager.GetById(targetUserId);
            if (targetUser == null)
            {
                throw new PapersBusinessException($"User with id {targetUserId} not found");
            }
            
            return SendResult.Success;
        }
    }
}
