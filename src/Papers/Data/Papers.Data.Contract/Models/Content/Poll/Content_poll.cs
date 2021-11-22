using System.Collections.Generic;

namespace Papers.Data.Contract.Models.Content.Poll
{
    public abstract class Content_poll
    {
        public abstract Content Content { get; set; }

        public abstract string AnswersIdsJson { get; set; }

        public abstract bool AllowMultiple { get; set; }

        public abstract IEnumerable<PoolAnswer> Answers { get; set; }
    }
}
