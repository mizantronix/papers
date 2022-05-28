namespace Papers.Data.MsSql.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Papers.Common.Enums;
    using Papers.Data.MsSql.Models;
    using Papers.Data.MsSql.Models.Content;
    using Papers.Data.MsSql.Configuration;

    public interface IMessageRepository
    {
        SendResult Send(User from, Chat chat, Message msg);
    }
    
    internal class MessageRepository : IMessageRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly DataContext _dataContext;

        public MessageRepository(IUserRepository userRepository, DataContext dataContext)
        {
            this._userRepository = userRepository;
            this._dataContext = dataContext;
        }

        public SendResult Send(User from, Chat chat, Message message)
        {
            var user = this._dataContext.Users.FirstOrDefault(u => u.Id == @from.Id);
            if (user == null)
            {
                this._dataContext.Users.Add((User) @from);
                this._dataContext.SaveChanges();
                user = this._dataContext.Users.FirstOrDefault(u => u.Id == @from.Id);
            }

            var chatToSend = this._dataContext.Chats.FirstOrDefault(c => c.Id == chat.Id);
            if (chatToSend == null)
            {
                this._dataContext.Chats.Add((Chat) chat);
                this._dataContext.SaveChanges();
                chatToSend = this._dataContext.Chats.FirstOrDefault(c => c.Id == chat.Id);
            }

            var messageTo = new Message
            {
                Content = message.Content,
                Chat = chat,
                FromUser = user,
                Sent = message.Sent,
                Viewed = false
            };

            this._dataContext.Messages.Add(messageTo);
            this._dataContext.SaveChanges();
            
            return SendResult.Success;
        }
    }
}
