namespace Papers.Domain.Managers
{
    using Papers.Common.Enums;
    using Papers.Data.MsSql.Repositories;

    public interface IMessageManager
    {
        public SendResult Send(long chatId);

        void Test();
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

        public void Test()
        {
            var user = this.userRepository.GetDefault();
        }

        public SendResult Send(long chatId)
        {
            // TODO GetCurrent()
            var user = this.userRepository.GetDefault();

            var chat = this.chatRepository.GetChatById(chatId);
            if (chat == null)
            {
                return SendResult.ChatNotFound;
            }

            this.messageRepository.Send(user, chat, this.messageRepository.GenerateMessage());
            return SendResult.Success;
        }
    }
}
