using System.Collections.Generic;
using System.Linq;
using Papers.Domain.Models.Message;

namespace Papers.Domain.Helpers
{
    using Papers.Common.Enums;
    using Papers.Domain.Models.User;

    public static class DalToDomainExtensions
    {
        public static User ToDomainModel(this Data.MsSql.Models.User user)
        {
            return new User
            {
                Id = user.Id,
                LastOnlineDateTime = user.LastOnlineDateTime,
                PasswordHash = user.PasswordHash,
                RegisterDate = user.RegisterDate,
                State = user.UserState.ToEnumState(),
                UserInfo = new UserInfo
                {
                    FirstName = user.UserInfo.FirstName,
                    LastName = user.UserInfo.LastName,
                    Login = user.UserInfo.Login,
                    UserPhone = user.UserInfo.PhoneNumber
                }
            };
        }

        public static IEnumerable<Message> ToDomainModel(this IEnumerable<Data.MsSql.Models.Message> dalMessages)
        {
            var result =
                from dalMessage in dalMessages
                select new Message
                {
                    Id = dalMessage.Id, 
                    SenderId = dalMessage.FromUserId, 
                    SendDateTime = dalMessage.Sent,
                    MessageContents = dalMessage.Content.Select(
                        content => new MessageContent
                        {
                            Text = content.ContentText?.Text,
                            Title = content.ContentText?.Title,
                            Type = content.Type.ToEnumState()
                        })
                };

            return result;
        }
    }
}