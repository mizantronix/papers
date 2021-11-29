namespace Papers.Data.MsSql.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    
    public class UserInfo
    {
        public long Id { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string PhoneNumber { get; set; }
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
