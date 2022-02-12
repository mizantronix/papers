using System.Collections.Generic;

namespace Papers.Data.Contract.Models.Content.Poll
{
    public abstract class _content_poll
    {
        public abstract _content Content { get; set; }

        public abstract bool AllowMultiple { get; set; }

        public abstract IEnumerable<_pollAnswer> Answers { get; set; }
    }
}