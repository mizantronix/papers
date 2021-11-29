﻿namespace Papers.Data.MsSql.Models.Content
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Papers.Data.MsSql.Models.Content.Poll;

    public class Content
    {
        public long Id { get; set; }
        public int Type { get; set; }

        public Message Message { get; set; }
        public long MessageId { get; set; }

        public ContentText ContentText { get; set; }
        public ContentPicture ContentPicture { get; set; }
        public ContentPoll ContentPoll { get; set; }
    }
    
    internal class ContentConfiguration : EntityTypeConfiguration<Content>
    {
        public ContentConfiguration()
        {
            ToTable("Contents", "dbo");

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Type).IsRequired();

            this.HasRequired(c => c.Message)
                .WithMany(m => (ICollection<Content>) m.Content)
                .HasForeignKey(c => c.MessageId);
        }
    }
}
