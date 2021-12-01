using System.Collections.Generic;
using System.Configuration;

namespace Papers.Data.MsSql.Configuration
{
    using Microsoft.EntityFrameworkCore;

    using Papers.Data.MsSql.Models;
    using Papers.Data.MsSql.Models.Content;
    using Papers.Data.MsSql.Models.Content.Poll;

    internal sealed class DataContext : DbContext
    {
        // TODO mb separate main & secondary models (like messages/contents & users/chats/bla-bla)
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
#if DEBUG
            Database.EnsureDeleted();
#endif
            Database.EnsureCreated();
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
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString =
#if DEBUG
                "Server=localhost;Database=Papers;Trusted_Connection=True;";
#elif RELEASE
                "release connection string";
#endif
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            
            // Chat
            modelBuilder.Entity<Chat>().ToTable("Chats", "dbo");
            modelBuilder.Entity<Chat>().HasKey(c => c.Id);
            modelBuilder.Entity<Chat>().Property(c => c.IsPrivate).IsRequired();
            modelBuilder.Entity<Chat>().Property(c => c.IsSecret).IsRequired();
            modelBuilder.Entity<Chat>().Property(c => c.IsGroup).IsRequired();

            modelBuilder.Entity<Chat>().HasOne(c => c.MasterUser)
                .WithMany(u => u.OwnChats)
                .HasForeignKey(c => c.MasterUserId);

            // Message
            modelBuilder.Entity<Message>().ToTable("Messages", "dbo");

            modelBuilder.Entity<Message>().HasKey(m => m.Id);
            modelBuilder.Entity<Message>().Property(m => m.Sent).IsRequired();
            modelBuilder.Entity<Message>().Property(m => m.Viewed).IsRequired();

            modelBuilder.Entity<Message>().HasOne(m => m.FromUser)
                .WithMany(u => (ICollection<Message>)u.SentMessages)
                .HasForeignKey(m => m.FromUserId);
            modelBuilder.Entity<Message>().HasOne(m => m.Chat)
                .WithMany(c => (ICollection<Message>)c.Messages)
                .HasForeignKey(m => m.ChatId);

            // User
            modelBuilder.Entity<User>().ToTable("Users", "dbo");
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<User>().HasOne(u => u.UserInfo)
                .WithOne(ui => ui.User);

            // UserChat
            modelBuilder.Entity<UserChat>().ToTable("User_Chat_Xref", "dbo");
            modelBuilder.Entity<UserChat>().HasKey(uc => new { uc.ChatId, uc.UserId });

            modelBuilder.Entity<UserChat>().HasOne(uc => uc.User)
                .WithMany(u => (ICollection<UserChat>)u.UserChats)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserChat>().HasOne(uc => uc.Chat)
                .WithMany(c => (ICollection<UserChat>)c.UserChats)
                .HasForeignKey(uc => uc.ChatId);

            // UserInfo
            modelBuilder.Entity<UserInfo>().ToTable("UserInfo", "dbo");

            modelBuilder.Entity<UserInfo>().HasKey(ui => ui.Id);

            modelBuilder.Entity<UserInfo>().Property(ui => ui.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<UserInfo>().Property(ui => ui.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<UserInfo>().Property(ui => ui.Login).IsRequired().HasMaxLength(50);

            // TODO phone validation or new class or something
            modelBuilder.Entity<UserInfo>().Property(ui => ui.PhoneNumber).IsRequired().HasMaxLength(15);

            // Content
            modelBuilder.Entity<Content>().ToTable("Contents", "dbo");

            modelBuilder.Entity<Content>().HasKey(c => c.Id);
            modelBuilder.Entity<Content>().Property(c => c.Type).IsRequired();

            modelBuilder.Entity<Content>().HasOne(c => c.Message)
                .WithMany(m => (ICollection<Content>)m.Content)
                .HasForeignKey(c => c.MessageId);

            // ContentText
            modelBuilder.Entity<ContentText>().ToTable("Content_Text", "dbo");

            modelBuilder.Entity<ContentText>().HasKey(ct => ct.Id);
            modelBuilder.Entity<ContentText>().Property(ct => ct.Text).IsRequired();
            modelBuilder.Entity<ContentText>().Property(ct => ct.Title).IsRequired(false).HasMaxLength(100);

            modelBuilder.Entity<ContentText>().HasOne(ct => ct.Content).WithOne(c => c.ContentText).HasForeignKey<Content>(ct => ct.MessageId);

            // ContentPicture
            modelBuilder.Entity<ContentPicture>().ToTable("Content_Picture", "dbo");

            modelBuilder.Entity<ContentPicture>().HasKey(cp => cp.Id);
            modelBuilder.Entity<ContentPicture>().Property(cp => cp.Data).IsRequired();
            modelBuilder.Entity<ContentPicture>().Property(cp => cp.Title).IsRequired(false).HasMaxLength(100);
            
            modelBuilder.Entity<ContentPicture>().HasOne(cp => cp.Content).WithOne(c => c.ContentPicture).HasForeignKey<Content>(ct => ct.MessageId);

            // ContentPoll
            modelBuilder.Entity<ContentPoll>().ToTable("Content_Poll", "dbo");

            modelBuilder.Entity<ContentPoll>().HasKey(cp => cp.Id);
            modelBuilder.Entity<ContentPoll>().Property(cp => cp.AllowMultiple).IsRequired();
            modelBuilder.Entity<ContentPoll>().HasOne(cp => (Content)cp.Content).WithOne(c => c.ContentPoll).HasForeignKey<Content>(ct => ct.MessageId);

            // UserPollAnswer
            modelBuilder.Entity<UserPollAnswer>().ToTable("Content_Poll_UserAnswer_Xref", "dbo");
            modelBuilder.Entity<UserPollAnswer>().HasKey(ua => ua.Id);
            modelBuilder.Entity<UserPollAnswer>().HasOne(ua => (ContentPoll)ua.Poll)
                .WithMany(cp => (ICollection<UserPollAnswer>)cp.UserPollAnswers)
                .HasForeignKey(c => c.PollId);

            modelBuilder.Entity<UserPollAnswer>().HasOne(ua => (User)ua.User)
                .WithMany(u => (ICollection<UserPollAnswer>)u.UserPollAnswers)
                .HasForeignKey(c => c.UserId);

            // PollAnswer
            modelBuilder.Entity<PollAnswer>().ToTable("Content_Poll_Answer", "dbo");
            modelBuilder.Entity<PollAnswer>().HasKey(cpa => cpa.Id);
            modelBuilder.Entity<PollAnswer>().HasOne(cpa => cpa.ContentPoll)
                .WithMany(cp => (ICollection<PollAnswer>)cp.Answers)
                .HasForeignKey(c => c.ContentPollId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
