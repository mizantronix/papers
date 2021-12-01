using System;

namespace Papers.Data.MsSql.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Papers.Data.MsSql.Models.Content.Poll;

    public class User
    {
        public long Id { get; set; }
        public UserInfo UserInfo { get; set; }
        public long UserInfoId { get; set; }
        public byte LastOnlineDeviceType { get; set; }
        public DateTime? LastOnlineDateTime { get; set; }
        public DateTime? RegisterDate { get; set; }
        public byte UserState { get; set; }
        public IEnumerable<Chat> OwnChats { get; set; }
        public IEnumerable<Message> SentMessages { get; set; }
        public IEnumerable<UserChat> UserChats { get; set; }
        public IEnumerable<UserPollAnswer> UserPollAnswers { get; set; }
    }
}
