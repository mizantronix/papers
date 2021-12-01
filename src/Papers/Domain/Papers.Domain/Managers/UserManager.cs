namespace Papers.Domain.Managers
{
    using Papers.Domain.Models.User;

    public interface IUserManager
    {
        public int Register(User user, UserInfo userInfo);
    }

    internal class UserManager : IUserManager
    {
    }
}
