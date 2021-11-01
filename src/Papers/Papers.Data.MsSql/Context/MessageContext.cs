using System.Data.Entity;
using Papers.Data.Contract.Models;

namespace Papers.Data.MsSql.Context
{
    public class MessageContext : DbContext
    {
        public MessageContext() : base()
        {
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Chat>
    }
}
