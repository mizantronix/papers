namespace Papers.Domain.Models.Message
{
    public class TextMessage : MessageContent
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string Sign { get; set; }
    }
}
