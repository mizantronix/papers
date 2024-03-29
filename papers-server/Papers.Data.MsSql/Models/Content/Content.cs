﻿namespace Papers.Data.MsSql.Models.Content
{
    using Papers.Data.MsSql.Models.Content.Poll;

    public class Content
    {
        public long Id { get; set; }
        public int Type { get; set; }

        public Message Message { get; set; }
        public long MessageId { get; set; }

        public ContentText ContentText { get; set; }
        public ContentPicture ContentPicture { get; set; }
        public ContentPoll ContentPoll { get; set; }
    }
}
