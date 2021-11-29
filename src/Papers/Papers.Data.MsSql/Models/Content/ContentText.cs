using System.ComponentModel.DataAnnotations.Schema;

namespace Papers.Data.MsSql.Models.Content
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.ModelConfiguration;

    public class ContentText
    {
        public long Id { get; set; }
        public Content Content { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
    }

    internal class ContentTextConfiguration : EntityTypeConfiguration<ContentText>
    {
        public ContentTextConfiguration()
        {
            ToTable("Content_Text", "dbo");

            this.HasKey(ct => ct.Id);
            this.Property(ct => ct.Text).IsRequired();
            this.Property(ct => ct.Title).IsOptional().HasMaxLength(100);
            
            this.HasRequired(ct => (Content) ct.Content).WithRequiredDependent(c => c.ContentText);
        }
    }
}
