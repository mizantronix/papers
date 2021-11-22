using System;
using System.Collections.Generic;

namespace Papers.Data.Contract.Models
{
    public abstract class Message
    {
        public abstract long Id { get; set; }

        public abstract User FromUser { get; set; }

        public abstract User ToUser { get; set; }

        public abstract DateTime Sent { get; set; }

        public abstract DateTime? Edited { get; set; }

        public abstract Chat Chat { get; set; }

        public abstract IEnumerable<Content.Content> Content { get; set; }

        public abstract bool Viewed { get; set; }
    }
}
