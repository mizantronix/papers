namespace Papers.Data.Contract.Repositories
{
    using Papers.Data.Contract.Models;

    public interface IUserRepository
    {
        public _user GetDefault();
    }
}
