namespace Papers.Domain.Managers
{
    using System;
    
    using Papers.Common.Enums;
    using Papers.Common.Exceptions;
    using Papers.Common.Helpers;

    using Papers.Data.MsSql.Repositories;
    using Papers.Domain.Models.User;

    public interface IUserManager
    {
        long Register(UserInfo userInfo, string password);

        long ConfirmUser(string phone, string code);

        User GetById(long id);

        User GetByIdentifier(string identifier);

        bool UserExists(long id);
    }

    internal class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public long Register(UserInfo userInfo, string password)
        {
            var user = this._userRepository.GetByPhone(userInfo.UserPhone);

            if (user == null)
            {
                user = this._userRepository.BeginRegistration(
                    userInfo.UserPhone, 
                    userInfo.Login, 
                    userInfo.FirstName,
                    userInfo.LastName, 
                    password.GetPasswordHash());
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
            return this._userRepository.ConfirmUser(phone).Id;

            /*
            var user = this._userRepository.GetByPhone(phone);
            if (user == null || CommonExtensions.GenerateConfirmCode(user.Id, phone, user.UserInfo.Login) != code)
            {
                throw new PapersBusinessException("Wrong confirm code");
            }

            // TODO Code verification
            return this._userRepository.ConfirmUser(phone).Id;*/
        }

        public User GetById(long id)
        {
            var user = this._userRepository.GetById(id);
            if (user == null)
            {
                return null;
            }

            return new User
            {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                LastOnlineDateTime = user.LastOnlineDateTime,
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

        public User GetByIdentifier(string identifier)
        {
            // TODO phone parsing
            var user = this._userRepository.GetByPhone(identifier) ?? this._userRepository.GetByLogin(identifier);

            return new User
            {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                LastOnlineDateTime = user.LastOnlineDateTime,
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

        public bool UserExists(long id)
        {
            return this._userRepository.UserExists(id);
        }
    }
}
