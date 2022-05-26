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
    }
}