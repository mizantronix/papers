﻿namespace Papers.Data.MsSql.Repositories
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Papers.Data.MsSql.Configuration;
    using Papers.Data.MsSql.Models;

    public interface IChatRepository
    {
        public Chat GetChatById(long id);
    }

    internal class ChatRepository : IChatRepository
    {
        private readonly DbContextOptions<DataContext> _contextOptions;

        public ChatRepository()
        {
            var opts = new DbContextOptionsBuilder<DataContext>();
            var connectionString =
#if DEBUG
                "Server=localhost;Database=Papers;Trusted_Connection=True;";
#elif RELEASE
                "release connection string";
#endif
            opts.UseSqlServer(connectionString);
            this._contextOptions = opts.Options;
        }

        public Chat GetChatById(long id)
        {
            using (var context = new DataContext(_contextOptions))
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
