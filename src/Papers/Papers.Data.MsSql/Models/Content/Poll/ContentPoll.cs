namespace Papers.Data.MsSql.Models.Content.Poll
{
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;

    using Papers.Data.Contract.Models.Content;
    using Papers.Data.Contract.Models.Content.Poll;

    internal class ContentPoll : _content_poll
    {
        public long Id => Content.Id;
        public override _content Content { get; set; }
        public override bool AllowMultiple { get; set; }
        public override IEnumerable<_pollAnswer> Answers { get; set; }
        public IEnumerable<UserPollAnswer> UserPollAnswers { get; set; }
    }

    internal class ContentPollConfiguration : EntityTypeConfiguration<ContentPoll>
    {
        public ContentPollConfiguration()
        {
            ToTable("Content_Poll", "dbo");

            this.Property(cp => cp.AllowMultiple).IsRequired();

            this.HasRequired(cp => (Content)cp.Content).WithRequiredDependent(c => c.ContentPoll);
        }
    }
}
