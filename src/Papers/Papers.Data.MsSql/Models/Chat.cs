namespace Papers.Data.MsSql.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Papers.Data.Contract.Models;

    internal class Chat : _chat
    {
        public override long Id { get; set; }
        public override bool IsPrivate { get; set; }
        public override bool IsSecret { get; set; }
        public IEnumerable<UserChat> UserChats { get; set; }
        public override bool IsGroup { get; set; }
        public override _user MasterUser { get; set; }
        public long? MasterUserId { get; set; }
        public override byte[] Picture { get; set; }
        public override IEnumerable<_message> Messages { get; set; }
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
