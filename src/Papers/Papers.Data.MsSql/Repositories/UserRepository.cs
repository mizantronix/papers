using System;
using System.Linq;
using Papers.Data.Contract.Models;
using Papers.Data.MsSql.Configuration;
using Papers.Data.MsSql.Models;

namespace Papers.Data.MsSql.Repositories
{
    using Papers.Data.Contract.Repositories;

    internal class UserRepository : IUserRepository
    {
        public _user GetDefault()
        {
            using (var context = new DataContext())
            {
                var userInfo = context.UserInfo.FirstOrDefault(ui =>
                    ui.Login.Equals("mizantronix", StringComparison.InvariantCultureIgnoreCase));

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
    }
}
