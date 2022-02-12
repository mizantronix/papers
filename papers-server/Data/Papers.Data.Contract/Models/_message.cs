namespace Papers.Data.Contract.Models
{
    using System;
    using System.Collections.Generic;

    public abstract class _message
    {
        public abstract long Id { get; set; }

        public abstract _user FromUser { get; set; }
        
        public abstract DateTime Sent { get; set; }

        public abstract DateTime? Edited { get; set; }

        public abstract _chat Chat { get; set; }

        public abstract IEnumerable<Content._content> Content { get; set; }

        public abstract bool Viewed { get; set; }
    }
}
