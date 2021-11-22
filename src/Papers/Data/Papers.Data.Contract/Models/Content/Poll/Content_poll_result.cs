namespace Papers.Data.Contract.Models.Content.Poll
{
    public abstract class Content_poll_result
    {
        public abstract User User { get; set; }

        public abstract Content_poll Poll { get; set; }

        public abstract string AnswersIdsJson { get; set; }
    }
}
