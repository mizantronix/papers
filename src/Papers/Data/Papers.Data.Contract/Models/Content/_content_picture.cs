namespace Papers.Data.Contract.Models.Content
{
    public abstract class _content_picture
    {
        public abstract _content Content { get; set; }

        public abstract byte[] Data { get; set; }

        public abstract string Title { get; set; }
    }
}
