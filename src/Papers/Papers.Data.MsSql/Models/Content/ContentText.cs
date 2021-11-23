using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Papers.Data.Contract.Models.Content;

namespace Papers.Data.MsSql.Models.Content
{
    internal class ContentText : _content_text
    {
        public override _content Content { get; set; }
        public override string Text { get; set; }
        public override string Title { get; set; }
    }

    internal class ContentTextConfiguration : EntityTypeConfiguration<ContentText>
    {
        public ContentTextConfiguration()
        {
            ToTable("Content_Text", "dbo");
            
            this.Property(ct => ct.Text).IsRequired();
            this.Property(ct => ct.Title).IsOptional().HasMaxLength(100);

            this.HasRequired(ct => (Content) ct.Content).WithRequiredDependent(c => c.ContentText);
        }
    }
}
