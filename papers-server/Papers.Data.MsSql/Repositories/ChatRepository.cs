namespace Papers.Data.MsSql.Repositories
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Papers.Common.Enums;
    using Papers.Data.MsSql.Configuration;
    using Papers.Data.MsSql.Models;

    public interface IChatRepository
    {
        Chat GetChatById(long id);

        Chat GetSingleChatByMemberIds(long id1, long id2);

        long CreateSingleChat(long creatorId, long targetId, bool isPrivate);
    }

    internal class ChatRepository : IChatRepository
    {
        private readonly DataContext _dataContext;

        public ChatRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public Chat GetChatById(long id)
        {
            var chat = this._dataContext.Chats.FirstOrDefault(c => c.Id == id);
            return chat;
        }

        public Chat GetSingleChatByMemberIds(long id1, long id2)
        {
            var chats = this._dataContext.Chats.Include(c => c.UserChats).Where(
                c => !c.IsGroup && (c.MasterUserId == id1 || c.MasterUserId == id2));

            foreach (var chat in chats)
            {
                if (chat.UserChats.Any(uc => uc.UserId == id1) && chat.UserChats.Any(uc => uc.UserId == id2))
                {
                    return chat;
                }
            }

            return null;
        }

        public long CreateSingleChat(long creatorId, long targetId, bool isPrivate)
        {
            var creatorUser = this._dataContext.Users.First(u => u.Id == creatorId && u.UserState == UserState.Registered.ToByteState());
            var targetUser = this._dataContext.Users.First(u => u.Id == targetId && u.UserState == UserState.Registered.ToByteState());
            var chat = new Chat
            {
                MasterUser = creatorUser,
                IsGroup = false,
                IsPrivate = isPrivate,
                IsSecret = false,
                MasterUserId = creatorId
            };

            this._dataContext.Chats.Add(chat);
            this._dataContext.SaveChanges();

            var userChat1 = new UserChat {Chat = chat, User = creatorUser};
            var userChat2 = new UserChat {Chat = chat, User = targetUser};

            chat.UserChats = new[] {userChat1, userChat2};
            this._dataContext.UserChats.AddRange(userChat1, userChat2);

            this._dataContext.SaveChanges();
            return chat.Id;
        }
    }
}
