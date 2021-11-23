using System;
using Papers.Data.Contract.Models;
using Papers.Domain.Models.User;

namespace Papers.Domain.Managers
{
    using Papers.Common.Contract.Enums;
    using Papers.Data.Contract.Repositories;
    using Papers.Domain.Models.Message;

    public interface IMessageManager
    {
        public SendResult Send(long chatId);
    }

    internal class MessageManager : IMessageManager
    {
        private readonly IMessageRepository messageRepository;
        private readonly IChatRepository chatRepository;
        private readonly IUserRepository userRepository;

        public MessageManager(IMessageRepository messageRepository, IChatRepository chatRepository, IUserRepository userRepository)
        {
            this.messageRepository = messageRepository;
            this.chatRepository = chatRepository;
            this.userRepository = userRepository;
        }

        public SendResult Send(long chatId)
        {
            // TODO GetCurrent()
            var user = this.userRepository.GetDefault();

            var chat = this.chatRepository.GetChatById(chatId);

            this.messageRepository.Send(user, chat, this.messageRepository.GenerateMessage());
            return SendResult.Success;
        }
    }
}
