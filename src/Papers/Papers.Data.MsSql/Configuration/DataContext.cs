namespace Papers.Data.MsSql.Configuration
{
    using System.Data.Entity;
    using Papers.Data.MsSql.Models;
    using Papers.Data.MsSql.Models.Content;
    using Papers.Data.MsSql.Models.Content.Poll;

    internal class DataContext : DbContext
    {
        // TODO mb separate main & secondary models (like messages/contents & users/chats/bla-bla)
        public DataContext() : base("DataContext")
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<ContentText> ContentTexts { get; set; }
        public DbSet<ContentPicture> ContentPictures { get; set; }
        public DbSet<ContentPoll> ContentPolls { get; set; }
        public DbSet<PollAnswer> PollAnswers { get; set; }
        public DbSet<UserPollAnswer> UserPollAnswers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
