using System.Collections.Generic;

namespace Papers.Data.Contract.Models
{
    public abstract class _user
    {
        public abstract long Id { get; set; }

        public abstract _userInfo UserInfo { get; set; }

        public abstract byte LastOnlineDeviceType { get; set; }
        
        public abstract IEnumerable<_chat> OwnChats { get; set; }

        public abstract IEnumerable<_message> SentMessages { get; set; }
    }
}
