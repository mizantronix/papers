namespace Papers.Data.MsSql.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Chat 
    {
        public long Id { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsSecret { get; set; }
        public IEnumerable<UserChat> UserChats { get; set; }
        public bool IsGroup { get; set; }
        public User MasterUser { get; set; }
        public long? MasterUserId { get; set; }
        public byte[] Picture { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }

    internal class ChatConfiguration : EntityTypeConfiguration<Chat>
    {
        public ChatConfiguration()
        {
            ToTable("Chats", "dbo");

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.IsPrivate).IsRequired();
            this.Property(c => c.IsSecret).IsRequired();
            this.Property(c => c.IsGroup).IsRequired();
            this.Property(c => c.Picture).IsOptional();

            this.HasOptional(c => c.MasterUser)
                .WithMany(u => (ICollection<Chat>) u.OwnChats)
                .HasForeignKey(c => c.MasterUserId);
        }
    }
}
