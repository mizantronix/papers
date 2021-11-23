using System.ComponentModel.DataAnnotations.Schema;

namespace Papers.Data.MsSql.Models
{
    using System;
    using System.Collections.Generic;
    using Papers.Data.Contract.Models;
    using Papers.Data.Contract.Models.Content;
    using System.Data.Entity.ModelConfiguration;

    internal class Message : _message
    {
        public override long Id { get; set; }
        public override _user FromUser { get; set; }
        public long FromUserId { get; set; }
        public override DateTime Sent { get; set; }
        public override DateTime? Edited { get; set; }
        public override _chat Chat { get; set; }
        public long ChatId { get; set; }
        public override IEnumerable<_content> Content { get; set; }
        public override bool Viewed { get; set; }
    }

    internal class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            ToTable("Messages", "dbo");
            
            this.Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Sent).IsRequired();
            this.Property(m => m.Viewed).IsRequired();
            this.Property(m => m.Edited).IsOptional();

            this.HasRequired(m => m.FromUser)
                .WithMany(u => (ICollection<Message>) u.SentMessages)
                .HasForeignKey(m => m.FromUserId);
            this.HasRequired(m => m.Chat)
                .WithMany(c => (ICollection<Message>) c.Messages)
                .HasForeignKey(m => m.ChatId);
        }
    }
}
