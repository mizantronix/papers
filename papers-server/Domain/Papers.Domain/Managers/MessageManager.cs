namespace Papers.Domain.Managers
{
    using System;
    using System.Collections.Generic;

    using Papers.Common.Enums;
    using Papers.Common.Exceptions;
    using Papers.Data.MsSql.Models.Content;
    using Papers.Data.MsSql.Repositories;
    using Papers.Domain.Models.Message;

    public interface IMessageManager
    {
        SendResult Send(long senderId, long chatId, Message message);
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

        public SendResult Send(long senderId, long chatId, Message message)
        {
            var user = this.userRepository.GetById(senderId);
            if (user == null)
            {
                throw new PapersBusinessException($"User with id {senderId} not found");
            }
            
            var chat = this.chatRepository.GetChatById(chatId);
            if (chat == null)
            {
                throw new PapersBusinessException($"Chat with id {senderId} not found");;
            }

            var dalMsg = new Data.MsSql.Models.Message
            {
                Chat = chat,
                FromUser = user,
                Sent = DateTime.Now,
                Viewed = false,
            };

            var dalContexts = new List<Content>();
            foreach (var content in message.MessageContents)
            {
                switch (content.Type)
                {
                    case MessageContentType.Text:
                        dalContexts.Add(new Content
                        {
                            Message = dalMsg,
                            Type = MessageContentType.Text.ToIntContentType(),
                            ContentText = new ContentText
                            {
                                Text = content.Text,
                                Title = content.Title
                            }
                        });
                        break;
                    case MessageContentType.Picture:
                        break;
                    case MessageContentType.Poll:
                        break;
                    default:
                        break;
                }
            }

            dalMsg.Content = dalContexts;

            this.messageRepository.Send(user, chat, dalMsg);
            return SendResult.Success;
        }
    }
}
