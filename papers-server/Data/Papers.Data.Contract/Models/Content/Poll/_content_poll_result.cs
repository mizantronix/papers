namespace Papers.Data.Contract.Models.Content.Poll
{
    public abstract class _content_poll_result
    {
        public abstract _user User { get; set; }

        public abstract _content_poll Poll { get; set; }
    }
}
