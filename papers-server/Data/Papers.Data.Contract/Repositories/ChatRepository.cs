namespace Papers.Data.Contract.Repositories
{
    using Papers.Data.Contract.Models;

    public interface IChatRepository
    {
        public _chat GetChatById(long id);
    }
}
