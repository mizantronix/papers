using System;
using System.Linq;
using Papers.Data.MsSql.Models;
using Papers.Data.MsSql.Models.Content;
using Papers.Data.MsSql.Models.Content.Poll;

namespace Papers.Data.MsSql.Repositories
{
    using Papers.Common.Contract.Enums;
    using Papers.Data.Contract.Models;
    using Papers.Data.Contract.Repositories;
    using Papers.Data.MsSql.Configuration;


    internal class MessageRepository : IMessageRepository
    {
        private readonly IUserRepository userRepository;

        public MessageRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public SendResult Send(_user @from, _chat chat, _message message)
        {
            using (var context = new DataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == @from.Id);
                if (user == null)
                {
                    context.Users.Add((User) @from);
                    context.SaveChanges();
                    user = context.Users.FirstOrDefault(u => u.Id == @from.Id);
                }

                var chatToSend = context.Chats.FirstOrDefault(c => c.Id == chat.Id);
                if (chatToSend == null)
                {
                    context.Chats.Add((Chat) chat);
                    context.SaveChanges();
                    chatToSend = context.Chats.FirstOrDefault(c => c.Id == chat.Id);
                }

                Message messageTo = new Message
                {
                    Content = message.Content,
                    Chat = chat,
                    FromUser = user,
                    Sent = message.Sent,
                    Viewed = false
                };

                context.Messages.Add(messageTo);
                foreach (Content contentTo in messageTo.Content)
                {
                    if (contentTo.ContentText != null)
                    {
                        context.ContentTexts.Add(new ContentText
                        {
                            Content = contentTo, 
                            Text = contentTo.ContentText.Text, 
                            Title = contentTo.ContentText.Title
                        });
                    }

                    if (contentTo.ContentPicture != null)
                    {
                        context.ContentPictures.Add(new ContentPicture
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

                context.SaveChanges();
            }

            return SendResult.Success;
        }

        public _message GenerateMessage()
        {
            var user = this.userRepository.GetDefault();
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
