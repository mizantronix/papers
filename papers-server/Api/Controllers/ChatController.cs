namespace Papers.Api.Controllers
{
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Mvc;

    using Papers.Api.Models;
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
        [Route("createSingleChat")]
        public SendResult CreateSingleChat(long creatorId, long targetUserId, bool isPrivate)
        {
            if (!this._userManager.UserExists(creatorId))
            {
                throw new PapersBusinessException($"Creator user with id {creatorId} not found");
            }

            if (!this._userManager.UserExists(targetUserId))
            {
                throw new PapersBusinessException($"Target user with id {targetUserId} not found");
            }

            this._chatManager.CreateSingleChat(creatorId, targetUserId, isPrivate);
            
            return SendResult.Success;
        }

        [HttpPost]
        [Route("createGroupChat")]
        public SendResult CreateGroupChat(GroupChatCreatingModel model)
        {
            if (model == null)
            {
                throw new PapersBusinessException("Chat creating model is empty");
            }

            if (!this._userManager.UserExists(model.CreatorId))
            {
                throw new PapersBusinessException($"Creator user with id {model.CreatorId} not found");
            }

            var errorSb = new StringBuilder("");
            if (model.MemberIds != null)
            {
                foreach (var memberId in model.MemberIds)
                {
                    if (!this._userManager.UserExists(memberId))
                    {
                        errorSb.AppendLine(memberId.ToString());
                    }
                }

                if (!string.IsNullOrEmpty(errorSb.ToString()))
                {
                    throw new PapersBusinessException($"Member users not found: {errorSb}");
                }
            }

            this._chatManager.CreateGroupChat(model.CreatorId, model.IsPrivate, model.MemberIds, model.Picture);
            return SendResult.Success;
        }

        [HttpGet]
        [Route("{id}")]
        public SendResult GetChatById(long id)
        {
            var chat = this._chatManager.GetById(id);
            return SendResult.Success;
        }

        [HttpPost]
        [Route("{chatId}/addUsers")]
        public SendResult AddUsers(long chatId, long[] ids)
        {
            var chat = this._chatManager.GetById(chatId);
            if (chat == null)
            {
                throw new PapersModelException($"Chat with id {chatId} not found");
            }
            
            if (chat.Users.Select(u => u.Id).Any(ids.Contains))
            {
                var existingIds = chat.Users.Select(u => u.Id).Where(ids.Contains).ToList();
                var sb = new StringBuilder();
                existingIds.ForEach(a => sb.Append($" {a}"));

                throw new PapersBusinessException($"Users already in chat:{sb}");
            }

            this._chatManager.AddUsersInGroupChat(chatId, ids);
            return SendResult.Success;
        }
    }
}
