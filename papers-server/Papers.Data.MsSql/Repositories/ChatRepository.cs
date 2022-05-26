using System.Collections.Generic;
using Papers.Common.Exceptions;

namespace Papers.Data.MsSql.Repositories
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Papers.Common.Enums;
    using Papers.Data.MsSql.Configuration;
    using Papers.Data.MsSql.Models;

    public interface IChatRepository
    {
        Chat GetChatById(long id);

        Chat GetChatByCreatorId(long id);

        Chat GetSingleChatByMemberIds(long id1, long id2);

        Chat CreateGroupChat(long creatorId, bool isPrivate, byte[] picture);

        long CreateSingleChat(long creatorId, long targetId, bool isPrivate);

        void AddUsers(long chatId, long[] memberIds);
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
            var chat = this._dataContext.Chats
                .Include(c => c.MasterUser)
                .ThenInclude(mu => mu.UserInfo)
                .Include(c => c.UserChats)
                .ThenInclude(uc => uc.User)
                .ThenInclude(u => u.UserInfo)
                .FirstOrDefault(c => c.Id == id);
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
            var creatorUser = this._dataContext.Users.FirstOrDefault(u => u.Id == creatorId && u.UserState == UserState.Registered.ToByteState());
            if (creatorUser == null)
            {
                throw new PapersModelException($"User with id {creatorId} not found");
            }

            var targetUser = this._dataContext.Users.FirstOrDefault(u => u.Id == targetId && u.UserState == UserState.Registered.ToByteState());
            if (targetUser == null)
            {
                throw new PapersModelException($"User with id {targetId} not found");
            }

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

        public Chat GetChatByCreatorId(long id)
        {
            var chat = this._dataContext.Chats.FirstOrDefault(c => c.MasterUserId == id);
            return chat;
        }

        public Chat CreateGroupChat(long creatorId, bool isPrivate, byte[] picture)
        {
            var user = this._dataContext.Users.FirstOrDefault(u => u.Id == creatorId);
            if (user == null)
            {
                throw new PapersModelException($"User with id {creatorId} not found");
            }

            var chat = new Chat
            {
                MasterUser = user,
                IsGroup = true,
                IsPrivate = isPrivate,
                Picture = picture,
                IsSecret = false
            };

            this._dataContext.Chats.Add(chat);
            this._dataContext.SaveChanges();

            this._dataContext.UserChats.Add(new UserChat {User = user, Chat = chat});
            this._dataContext.SaveChanges();

            return chat;
        }

        public void AddUsers(long chatId, long[] memberIds)
        {
            var chat = this._dataContext.Chats.FirstOrDefault(c => c.Id == chatId);
            if (chat == null)
            {
                throw new PapersModelException($"Chat with id {chatId} not found");
            }

            var userChatList = new List<UserChat>();

            foreach (var memberId in memberIds)
            {
                var user = this._dataContext.Users.FirstOrDefault(u => u.Id == memberId);
                if (user == null)
                {
                    throw new PapersModelException($"User with id {memberId} not found");
                }

                userChatList.Add(new UserChat {User = user, Chat = chat});
            }

            if (chat.UserChats == null)
            {
                chat.UserChats = userChatList;
            }
            else
            {
                var tmpList = chat.UserChats.ToList();
                tmpList.AddRange(userChatList);
                chat.UserChats = tmpList;
            }
            this._dataContext.UserChats.AddRange(userChatList);

            this._dataContext.SaveChanges();
        }
    }
}
