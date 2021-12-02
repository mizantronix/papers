namespace Papers.Data.MsSql.Models
{
    public class UserInfo
    {
        public long Id { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Picture { get; set; }
    }
}
