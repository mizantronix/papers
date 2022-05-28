namespace Papers.Domain.Models.Message
{
    using Papers.Common.Enums;

    public class MessageContent
    {
        public MessageContentType Type { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Sign { get; set; }
    }
}
