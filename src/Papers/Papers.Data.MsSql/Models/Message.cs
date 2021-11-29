﻿namespace Papers.Data.MsSql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    
    public class Message
    {
        public long Id { get; set; }
        public User FromUser { get; set; }
        public long FromUserId { get; set; }
        public DateTime Sent { get; set; }
        public DateTime? Edited { get; set; }
        public Chat Chat { get; set; }
        public long ChatId { get; set; }
        public IEnumerable<Content.Content> Content { get; set; }
        public bool Viewed { get; set; }
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
