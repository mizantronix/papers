namespace Papers.Data.MsSql.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Message
    {
        public long Id { get; set; }
        public User FromUser { get; set; }
        public long FromUserId { get; set; }
        public DateTime Sent { get; set; }
        public DateTime? Edited { get; set; }
        public Chat Chat { get; set; }
        public long ChatId { get; set; }
        public IEnumerable<Content.Content> Content { get; set; }
        public bool Viewed { get; set; }
    }
}
