namespace Papers.Data.MsSql.Context
{
    using System.Data.Entity;
    using Papers.Data.Contract.Models;


    public class MessageContext : DbContext
    {
        public MessageContext() : base()
        {
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Chat>
    }
}
