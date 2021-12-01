﻿namespace Papers.Data.MsSql.Repositories
{
    using System;
    using System.Linq;
    using System.Configuration;

    using Microsoft.EntityFrameworkCore;

    using Papers.Common.Contract.Enums;
    using Papers.Data.MsSql.Configuration;
    using Papers.Data.MsSql.Models;

    public interface IUserRepository
    {
        User GetDefault();

        User Register();

        User GetByPhone(string phone);

        User BeginRegistration(string phone, string login, string firstName, string lastName);

        User ContinueRegistration(string phone, string login, string firstName, string lastName);

        User ConfirmUser(string phone);
    }

    internal class UserRepository : IUserRepository
    {
        private readonly DbContextOptions<DataContext> _contextOptions;

        public UserRepository()
        {
            var opts = new DbContextOptionsBuilder<DataContext>();
            var test1 = ConfigurationManager.ConnectionStrings["DataContext"];
            var connectionString =
#if DEBUG
                "Server=localhost;Database=Papers;Trusted_Connection=True;";
#elif RELEASE
                "release connection string";
#endif
            opts.UseSqlServer(connectionString);
            this._contextOptions = opts.Options;
        }

        public User GetDefault()
        {
            using (var context = new DataContext(this._contextOptions))
            {
                var userInfo = context.UserInfo.FirstOrDefault(ui => ui.Login == "mizantronix");

                if (userInfo == null)
                {
                    userInfo = new UserInfo
                    {
                        Login = "mizantronix",
                        FirstName = "Andrey",
                        LastName = "G.",
                        PhoneNumber = "+79999999999"
                    };
                    context.UserInfo.Add(userInfo);

                    context.SaveChanges();
                }

                var user = context.Users.FirstOrDefault(u => u.UserInfo.Id == userInfo.Id);

                if (user == null)
                {
                    user = new User
                    {
                        LastOnlineDeviceType = 10,
                        UserInfo = userInfo,
                    };

                    context.Users.Add(user);
                    context.SaveChanges();
                }

                return user;
            }
        }

        public User Register()
        {
            throw new NotImplementedException();
        }

        public User GetByPhone(string phone)
        {
            using (var context = new DataContext(this._contextOptions))
            {
                return context.UserInfo.FirstOrDefault(ui => ui.PhoneNumber == phone)?.User;
            }
        }

        public User BeginRegistration(string phone, string login, string firstName, string lastName)
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
                }
            };

            using (var context = new DataContext(this._contextOptions))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return user;
        }

        public User ContinueRegistration(string phone, string login, string firstName, string lastName)
        {
            using (var context = new DataContext(this._contextOptions))
            {
                var user = context.UserInfo.First(ui => ui.PhoneNumber == phone).User;
                
                user.RegisterDate = DateTime.Now;

                user.UserInfo.FirstName = firstName;
                user.UserInfo.LastName = lastName;
                user.UserInfo.Login = login;
                user.UserState = UserState.NeedVerification.ToByteState();

                context.SaveChanges();

                return user;
            }
        }

        public User ConfirmUser(string phone)
        {
            using (var context = new DataContext(this._contextOptions))
            {
                var user = context.UserInfo.First(ui => ui.PhoneNumber == phone).User;
                user.UserState = UserState.Registered.ToByteState();
                if (user.RegisterDate == null)
                {
                    user.RegisterDate = DateTime.Now;
                }

                context.SaveChanges();

                return user;
            }
        }
    }
}
