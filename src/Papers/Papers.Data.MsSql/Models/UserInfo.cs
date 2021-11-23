using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Papers.Data.MsSql.Models
{
    using Papers.Data.Contract.Models;

    internal class UserInfo : _userInfo
    {
        public override long Id { get; set; }
        public override _user User { get; set; }
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override string Login { get; set; }
        public override string PhoneNumber { get; set; }
    }
    
    internal class UserInfoConfiguration : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoConfiguration()
        {
            ToTable("UserInfo", "dbo");

            this.Property(ui => ui.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(ui => ui.FirstName).IsRequired().HasMaxLength(50);
            this.Property(ui => ui.LastName).IsRequired().HasMaxLength(50);
            this.Property(ui => ui.Login).IsRequired().HasMaxLength(50);

            // TODO phone validation or new class or something
            this.Property(ui => ui.PhoneNumber).IsRequired().HasMaxLength(15);
        }
    }
}
