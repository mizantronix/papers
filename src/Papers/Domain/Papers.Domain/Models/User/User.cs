namespace Papers.Domain.Models.User
{
    using System;

    using Papers.Common.Enums;

    public class User
    {
        public long Id { get; set; }

        public UserState State { get; set; }

        public DateTime? LastOnlineDateTime { get; set; }

        public DateTime? RegisterDate { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
