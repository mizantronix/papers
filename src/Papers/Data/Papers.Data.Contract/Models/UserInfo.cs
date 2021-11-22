namespace Papers.Data.Contract.Models
{
    public abstract class UserInfo
    {
        public abstract long Id { get; set; }

        public abstract string FirstName { get; set; }

        public abstract string LastName { get; set; }

        public abstract string Login { get; set; }

        public abstract string PhoneNumber { get; set; }
    }
}
