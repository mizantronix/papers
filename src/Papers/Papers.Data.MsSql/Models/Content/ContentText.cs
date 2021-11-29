namespace Papers.Data.MsSql.Models.Content
{

    public class ContentText
    {
        public long Id { get; set; }
        public Content Content { get; set; }
        public long ContentId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
    }
}
