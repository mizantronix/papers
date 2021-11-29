namespace Papers.Data.MsSql.Models.Content.Poll
{
    public class UserPollAnswer
    {
        public long Id { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }

        public ContentPoll Poll { get; set; }
        public long PollId { get; set; }
    }
}
