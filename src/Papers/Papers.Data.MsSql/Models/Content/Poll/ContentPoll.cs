namespace Papers.Data.MsSql.Models.Content.Poll
{
    using System.Collections.Generic;
    
    public class ContentPoll
    {
        public long Id { get; set; }
        public Content Content { get; set; }
        public long ContentId { get; set; }
        public bool AllowMultiple { get; set; }
        public IEnumerable<PollAnswer> Answers { get; set; }
        public IEnumerable<UserPollAnswer> UserPollAnswers { get; set; }
    }
}
