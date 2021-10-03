using System;

namespace Papers.Data.Contract.Models
{
    using System.Collections.Generic;

    public abstract class Message
    {
        public long Id { get; set; }
        
        public long SenderId { get; set; }

        public DateTime SendDateTime { get; set; }

        public IEnumerable<MessageContent> MessageContents { get; set; }
    }
}
