namespace Papers.Domain.Models.User
{
    using System;

    public class User
    {
        public long Id { get; set; }

        public UserInfo UserInfo { get; set; }

        public DateTime? LastOnline { get; set; }
    }
}
