namespace Papers.Data.Contract.Models.Content
{
    public abstract class Content_picture
    {
        public abstract Content Content { get; set; }

        public abstract byte[] Data { get; set; }

        public abstract string Title { get; set; }
    }
}
