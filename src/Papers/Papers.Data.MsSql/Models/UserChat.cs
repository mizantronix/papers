namespace Papers.Data.MsSql.Models
{
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;

    internal class UserChat
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long ChatId { get; set; }
        public Chat Chat { get; set; }
    }

    internal class UserChatConfiguration : EntityTypeConfiguration<UserChat>
    {
        public UserChatConfiguration()
        {
            ToTable("User_Chat_Xref", "dbo");
            this.HasKey(uc => new {uc.ChatId, uc.UserId});

            this.HasRequired(uc => uc.User)
                .WithMany(u => (ICollection<UserChat>)u.UserChats)
                .HasForeignKey(uc => uc.UserId);

            this.HasRequired(uc => uc.Chat)
                .WithMany(c => (ICollection<UserChat>)c.UserChats)
                .HasForeignKey(uc => uc.ChatId);
        }
    }
}
