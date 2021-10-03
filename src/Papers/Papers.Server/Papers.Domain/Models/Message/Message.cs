namespace Papers.Domain.Models.Message
{
    using System;
    using System.Collections.Generic;

    public class Message
    {
        public long Id { get; set; }

        public long SenderId { get; set; }

        public DateTime SendDateTime { get; set; }

        public IEnumerable<MessageContent> MessageContents { get; set; }
    }
}
