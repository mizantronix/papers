using Papers.Data.MsSql.Models.Content.Poll;

namespace Papers.Data.MsSql.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Papers.Data.Contract.Models;

    internal class User : _user
    {
        public override long Id { get; set; }
        public override _userInfo UserInfo { get; set; }
        public long UserInfoId { get; set; }
        public override byte LastOnlineDeviceType { get; set; }
        public override IEnumerable<_chat> OwnChats { get; set; }
        public override IEnumerable<_message> SentMessages { get; set; }
        public IEnumerable<UserChat> UserChats { get; set; }
        public IEnumerable<UserPollAnswer> UserPollAnswers { get; set; }
    }

    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users", "dbo");
            this.Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(u => u.UserInfo)
                .WithRequiredDependent(ui => (User) ui.User);
        }
    }
}
