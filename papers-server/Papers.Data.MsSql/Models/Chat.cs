namespace Papers.Data.MsSql.Models
{
    using System.Collections.Generic;
    
    public class Chat 
    {
        public long Id { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsSecret { get; set; }
        public IEnumerable<UserChat> UserChats { get; set; }
        public bool IsGroup { get; set; }
        public User MasterUser { get; set; }
        public long? MasterUserId { get; set; }
        public byte[] Picture { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
