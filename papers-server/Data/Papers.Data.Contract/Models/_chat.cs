using System.Collections.Generic;

namespace Papers.Data.Contract.Models
{
    public abstract class _chat
    {
        public abstract long Id { get; set; }

        public abstract bool IsPrivate { get; set; }

        public abstract bool IsSecret { get; set; }
        
        public abstract bool IsGroup { get; set; }

        public abstract _user MasterUser { get; set; }

        public abstract byte[] Picture { get; set; }

        public abstract IEnumerable<_message> Messages { get; set; }
    }
}
