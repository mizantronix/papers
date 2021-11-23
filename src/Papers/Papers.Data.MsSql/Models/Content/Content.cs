using Papers.Data.MsSql.Models.Content.Poll;

namespace Papers.Data.MsSql.Models.Content
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Papers.Data.Contract.Models.Content;

    internal class Content : _content
    {
        public override long Id { get; set; }
        public override byte Type { get; set; }

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
