namespace Papers.Domain.Models.Chat
{
    public class GroupChat : Chat
    {
        public User.User MasterUser { get; set; }

        public byte[] Picture { get; set; }
    }
}