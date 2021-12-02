namespace Papers.Data.MsSql.Models.Content.Poll
{
    public class PollAnswer
    {
        public long Id { get; set; }
        public string Text { get; set; }
        
        public ContentPoll ContentPoll { get; set; }
        public long ContentPollId { get; set; }
    }
}
