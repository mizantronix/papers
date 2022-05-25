namespace Papers.Data.MsSql.Repositories
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using Papers.Common.Enums;
    using Papers.Common.Exceptions;
    using Papers.Data.MsSql.Configuration;
    using Papers.Data.MsSql.Models;

    public interface IUserRepository
    {
        User GetDefault();

        User Register();

        User GetByPhone(string phone);

        User GetByLogin(string login);

        User BeginRegistration(string phone, string login, string firstName, string lastName, int passwordHash);

        User ContinueRegistration(string phone, string login, string firstName, string lastName);

        User ConfirmUser(string phone);

        User GetById(long id, UserState? state = null);
    }

    internal class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public User GetDefault()
        {
            var userInfo = this._dataContext.UserInfo.FirstOrDefault(ui => ui.Login == "mizantronix");

            if (userInfo == null)
            {
                userInfo = new UserInfo
                {
                    Login = "mizantronix",
                    FirstName = "Andrey",
                    LastName = "G.",
                    PhoneNumber = "+79999999999"
                };
                this._dataContext.UserInfo.Add(userInfo);

                this._dataContext.SaveChanges();
            }

            var user = this._dataContext.Users.FirstOrDefault(u => u.UserInfo.Id == userInfo.Id);

            if (user == null)
            {
                user = new User
                {
                    LastOnlineDeviceType = 10,
                    UserInfo = userInfo,
                };

                this._dataContext.Users.Add(user);
                this._dataContext.SaveChanges();
            }

            return user;
        }

        public User Register()
        {
            throw new NotImplementedException();
        }

        public User GetByPhone(string phone)
        {
            var u = this._dataContext.Users.Include(u => u.UserInfo).FirstOrDefault(u => u.UserInfo.PhoneNumber == phone);
            return u;
        }

        public User BeginRegistration(string phone, string login, string firstName, string lastName, int passwordHash)
        {
            var user = new User
            {
                UserState = UserState.New.ToByteState(),
                UserInfo = new UserInfo
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Login = login,
                    PhoneNumber = phone
                },
                PasswordHash = passwordHash
            };

            if (this._dataContext.UserInfo.FirstOrDefault(ui => ui.Login == login) != null)
            {
                throw new PapersBusinessException("Login already exists");
            }

            this._dataContext.Users.Add(user);
            this._dataContext.SaveChanges();

            return user;
        }

        public User ContinueRegistration(string phone, string login, string firstName, string lastName)
        {
            var user = this._dataContext.Users.First(u => u.UserInfo.PhoneNumber == phone);

            user.RegisterDate = DateTime.Now;

            user.UserInfo.FirstName = firstName;
            user.UserInfo.LastName = lastName;
            user.UserInfo.Login = login;
            user.UserState = UserState.NeedVerification.ToByteState();

            this._dataContext.SaveChanges();

            return user;
        }

        public User ConfirmUser(string phone)
        {
            var user = this._dataContext.Users.First(u => u.UserInfo.PhoneNumber == phone);
            user.UserState = UserState.Registered.ToByteState();
            if (user.RegisterDate == null)
            {
                user.RegisterDate = DateTime.Now;
            }

            this._dataContext.SaveChanges();

            return user;
        }

        public User GetById(long id, UserState? state = null)
        {
            return state.HasValue
                ? this._dataContext.Users.Include(u => u.UserInfo).FirstOrDefault(u => u.Id == id && u.UserState == state.Value.ToByteState())
                : this._dataContext.Users.Include(u => u.UserInfo).FirstOrDefault(u => u.Id == id);
        }

        public User GetByLogin(string login)
        {
            var user = this._dataContext.Users.Include(u => u.UserInfo).FirstOrDefault(u => u.UserInfo.Login == login);
            return user;
        }
    }
}
