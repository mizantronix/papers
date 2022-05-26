namespace Papers.Api.Models
{
    public class GroupChatCreatingModel
    {
        public long CreatorId { get; set; }

        public bool IsPrivate { get; set; }

        public long[] MemberIds { get; set; }

        public byte[] Picture { get; set; }
    }
}
