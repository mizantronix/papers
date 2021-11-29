using System.ComponentModel.DataAnnotations.Schema;

namespace Papers.Data.MsSql.Models.Content
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.ModelConfiguration;
    
    public class ContentPicture
    {
        public long Id { get; set; }
        public Content Content { get; set; }
        public byte[] Data { get; set; }
        public string Title { get; set; }
    }

    internal class ContentPictureConfiguration : EntityTypeConfiguration<ContentPicture>
    {
        public ContentPictureConfiguration()
        {
            ToTable("Content_Picture", "dbo");

            this.HasKey(cp => cp.Id);
            this.Property(cp => cp.Data).IsRequired();
            this.Property(cp => cp.Title).IsOptional().HasMaxLength(100);

            this.HasRequired(cp => (Content)cp.Content).WithRequiredDependent(c => c.ContentPicture);
        }
    }
}
