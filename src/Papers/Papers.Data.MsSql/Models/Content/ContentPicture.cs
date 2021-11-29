namespace Papers.Data.MsSql.Models.Content
{
    
    public class ContentPicture
    {
        public long Id { get; set; }
        public Content Content { get; set; }
        public long ContentId { get; set; }
        public byte[] Data { get; set; }
        public string Title { get; set; }
    }
}
