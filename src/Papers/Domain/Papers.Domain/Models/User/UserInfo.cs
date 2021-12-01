namespace Papers.Domain.Models.User
{
    public class UserInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        // TODO отдельный класс, валидация, то-се
        public string UserPhone { get; set; }
    }
}
