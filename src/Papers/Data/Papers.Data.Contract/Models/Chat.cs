using System.Collections.Generic;

namespace Papers.Data.Contract.Models
{
    public abstract class Chat
    {
        public abstract long Id { get; set; }

        public abstract bool IsPrivate { get; set; }

        public abstract bool IsSecret { get; set; }

        public abstract IEnumerable<User> Users { get; set; }

        public abstract bool IsGroup { get; set; }

        public abstract User MasterUser { get; set; }

        public abstract byte[] Picture { get; set; }

        public abstract IEnumerable<Message> Messages { get; set; }
    }
}
