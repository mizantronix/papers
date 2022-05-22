namespace Papers.Data.MsSql.Repositories
{
    using System;
    using System.Linq;
    
    using Papers.Common.Enums;
    using Papers.Data.MsSql.Models;
    using Papers.Data.MsSql.Models.Content;
    using Papers.Data.MsSql.Configuration;

    public interface IMessageRepository
    {
        SendResult Send(User from, Chat chat, Message message);

        // TODO testing
        Message GenerateMessage();
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

            Message messageTo = new Message
            {
                Content = message.Content,
                Chat = chat,
                FromUser = user,
                Sent = message.Sent,
                Viewed = false
            };

            this._dataContext.Messages.Add(messageTo);
            foreach (Content contentTo in messageTo.Content)
            {
                if (contentTo.ContentText != null)
                {
                    this._dataContext.ContentTexts.Add(new ContentText
                    {
                        Content = contentTo,
                        Text = contentTo.ContentText.Text,
                        Title = contentTo.ContentText.Title
                    });
                }

                if (contentTo.ContentPicture != null)
                {
                    this._dataContext.ContentPictures.Add(new ContentPicture
                    {
                        Content = contentTo,
                        Title = contentTo.ContentPicture.Title,
                        Data = contentTo.ContentPicture.Data
                    });
                }

                if (contentTo.ContentPoll != null)
                {
                    // TODO
                }
            }

            this._dataContext.SaveChanges();

            return SendResult.Success;
        }

        public Message GenerateMessage()
        {
            var user = this._userRepository.GetDefault();
            var message = new Message
            {
                FromUser = user,
                Sent = DateTime.Now,
                Viewed = false,
                FromUserId = user.Id,
            };
            var content = new Content
            {
                Type = 1,
                ContentText = new ContentText
                {
                    Text = "test message",
                    Title = "test title"
                },
                Message = message
            };
            message.Content = new[] {content};
            return message;
        }
    }
}
