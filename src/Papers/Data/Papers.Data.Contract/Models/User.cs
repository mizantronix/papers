using System.Collections.Generic;

namespace Papers.Data.Contract.Models
{
    public abstract class User
    {
        public abstract long Id { get; set; }

        public abstract UserInfo UserInfo { get; set; }

        public abstract byte LastOnlineDeviceType { get; set; }

        public abstract IEnumerable<Chat> Chats { get; set; }

        public abstract IEnumerable<Message> SentMessages { get; set; }

        public abstract IEnumerable<Message> ReceivedMessages { get; set; }
    }
}
