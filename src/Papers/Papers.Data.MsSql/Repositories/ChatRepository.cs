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
        public Chat GetChatById(long id)
        {
            using (var context = new DataContext())
            {
                var chat = context.Chats.FirstOrDefault(c => c.Id == id);
                if (chat == null)
                {
                    chat = new Chat
                    {
                        IsGroup = false,
                        IsPrivate = false,
                        IsSecret = false
                    };

                    context.Chats.Add(chat);
                    context.SaveChanges();
                }

                return chat;
            }
        }
    }
}
