namespace Papers.Data.MsSql.Models.Content
{
    using System.Data.Entity.ModelConfiguration;

    using Papers.Data.Contract.Models.Content;

    internal class ContentPicture : _content_picture
    {
        public override _content Content { get; set; }
        public override byte[] Data { get; set; }
        public override string Title { get; set; }
    }

    internal class ContentPictureConfiguration : EntityTypeConfiguration<ContentPicture>
    {
        public ContentPictureConfiguration()
        {
            ToTable("Content_Picture", "dbo");

            this.Property(cp => cp.Data).IsRequired();
            this.Property(cp => cp.Title).IsOptional().HasMaxLength(100);

            this.HasRequired(cp => (Content)cp.Content).WithRequiredDependent(c => c.ContentPicture);
        }
    }
}
