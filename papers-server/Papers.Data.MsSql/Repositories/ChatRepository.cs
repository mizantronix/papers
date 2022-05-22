namespace Papers.Data.MsSql.Repositories
{
    using System.Linq;

    using Papers.Data.MsSql.Configuration;
    using Papers.Data.MsSql.Models;

    public interface IChatRepository
    {
        public Chat GetChatById(long id);
    }

    internal class ChatRepository : IChatRepository
    {
        private readonly DataContext _dataContext;

        public ChatRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public Chat GetChatById(long id)
        {
            var chat = this._dataContext.Chats.FirstOrDefault(c => c.Id == id);
            return chat;
        }
    }
}
