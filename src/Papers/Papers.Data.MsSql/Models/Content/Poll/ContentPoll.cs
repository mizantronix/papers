using System.ComponentModel.DataAnnotations.Schema;

namespace Papers.Data.MsSql.Models.Content.Poll
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.ModelConfiguration;
    
    public class ContentPoll
    {
        public long Id { get; set; }
        public Content Content { get; set; }
        public bool AllowMultiple { get; set; }
        public IEnumerable<PollAnswer> Answers { get; set; }
        public IEnumerable<UserPollAnswer> UserPollAnswers { get; set; }
    }

    internal class ContentPollConfiguration : EntityTypeConfiguration<ContentPoll>
    {
        public ContentPollConfiguration()
        {
            ToTable("Content_Poll", "dbo");

            this.HasKey(cp => cp.Id);
            this.Property(cp => cp.AllowMultiple).IsRequired();
            this.HasRequired(cp => (Content)cp.Content).WithRequiredDependent(c => c.ContentPoll);
        }
    }
}
