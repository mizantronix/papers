namespace Papers.Domain.Managers
{
    using System;
    
    using Papers.Common.Enums;
    using Papers.Common.Exceptions;
    using Papers.Data.MsSql.Repositories;
    using Papers.Domain.Models.User;

    public interface IUserManager
    {
        long Register(UserInfo userInfo);

        long ConfirmUser(string phone, string code);
    }

    internal class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public long Register(UserInfo userInfo)
        {
            var user = this._userRepository.GetByPhone(userInfo.UserPhone);

            if (user == null)
            {
                user = this._userRepository.BeginRegistration(
                    userInfo.UserPhone, 
                    userInfo.Login, 
                    userInfo.FirstName,
                    userInfo.LastName);
                // TODO Send confirm code

                return user.Id;
            }

            switch (user.UserState.ToEnumState())
            {
                case UserState.New:
                case UserState.Removed:
                    user = this._userRepository.ContinueRegistration(
                        userInfo.UserPhone, 
                        userInfo.Login, 
                        userInfo.FirstName, 
                        userInfo.LastName);
                    // TODO Send confirm code
                    break;
                case UserState.NeedVerification:
                    // TODO Send confirm code
                    break;
                case UserState.Registered:
                    throw new PapersBusinessException("User already registered");
                default:
                    throw new ArgumentOutOfRangeException($"userState = {user.UserState}");
            }

            return user.Id;
        }

        public long ConfirmUser(string phone, string code)
        {
            // TODO Code verification
            return this._userRepository.ConfirmUser(phone).Id;
        }
    }
}
