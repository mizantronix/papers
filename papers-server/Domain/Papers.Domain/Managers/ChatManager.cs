using Papers.Common.Enums;

namespace Papers.Domain.Managers
{
    using System.Linq;

    using Papers.Common.Exceptions;
    using Papers.Data.MsSql.Repositories;
    using Papers.Domain.Helpers;
    using Papers.Domain.Models.Chat;

    public interface IChatManager
    {
        Chat GetById(long id);

        long CreateSingleChat(long creatorId, long targetId, bool isPrivate);

        long CreateGroupChat(long creatorId, bool isPrivate, long[] memberIds = null, byte[] picture = null);

        void AddUsersInGroupChat(long chatId, long[] ids);
    }

    internal class ChatManager : IChatManager
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;

        public ChatManager(IChatRepository chatRepository, IUserRepository userRepository)
        {
            this._chatRepository = chatRepository;
            this._userRepository = userRepository;
        }

        public Chat GetById(long id)
        {
            var chat = this._chatRepository.GetChatById(id);
            if (chat == null)
            {
                return null;
            }

            var domainUsers = chat.UserChats.Select(userChat => userChat.User.ToDomainModel());

            return chat.IsGroup
                ? new GroupChat
                {
                    Id = chat.Id,
                    IsPrivate = chat.IsPrivate,
                    IsSecret = chat.IsSecret,
                    MasterUser = chat.MasterUser.ToDomainModel(),
                    Picture = chat.Picture,
                    Users = domainUsers
                }
                : new Chat
                {
                    Id = chat.Id,
                    IsPrivate = chat.IsPrivate,
                    IsSecret = chat.IsSecret,
                    Users = domainUsers
                };
        }

        public long CreateSingleChat(long creatorId, long targetId, bool isPrivate)
        {
            var chat = this._chatRepository.GetSingleChatByMemberIds(creatorId, targetId);
            if (chat != null)
            {
                throw new PapersBusinessException(
                    $"Single chat between users with id {creatorId} and {targetId} already exists");
            }

            if (this._userRepository.GetById(creatorId, UserState.Registered) == null)
            {
                throw new PapersBusinessException($"Registered user with id {creatorId} not found");
            }

            if (this._userRepository.GetById(targetId, UserState.Registered) == null)
            {
                throw new PapersBusinessException($"Registered user with id {targetId} not found");
            }

            return this._chatRepository.CreateSingleChat(creatorId, targetId, isPrivate);
        }

        public long CreateGroupChat(long creatorId, bool isPrivate, long[] memberIds = null, byte[] picture = null)
        {
            var chat = this._chatRepository.CreateGroupChat(creatorId, isPrivate, picture);
            if (memberIds != null)
            {
                this._chatRepository.AddUsers(chat.Id, memberIds);
            }

            return chat.Id;
        }

        public void AddUsersInGroupChat(long chatId, long[] ids)
        {
            this._chatRepository.AddUsers(chatId, ids);
        }
    }
}
