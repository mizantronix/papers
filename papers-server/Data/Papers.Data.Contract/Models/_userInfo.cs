namespace Papers.Data.Contract.Models
{
    public abstract class _userInfo
    {
        public abstract long Id { get; set; }

        public abstract _user User { get; set; }

        public abstract string FirstName { get; set; }

        public abstract string LastName { get; set; }

        public abstract string Login { get; set; }

        public abstract string PhoneNumber { get; set; }
    }
}
