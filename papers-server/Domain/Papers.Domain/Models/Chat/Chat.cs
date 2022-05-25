namespace Papers.Domain.Models.Chat
{
    using System.Collections.Generic;

    public class Chat
    {
        public long Id { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsSecret { get; set; }

        public IEnumerable<User.User> Users { get; set; }
    }
}