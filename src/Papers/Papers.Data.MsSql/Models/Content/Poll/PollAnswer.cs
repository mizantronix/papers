namespace Papers.Data.MsSql.Models.Content.Poll
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Papers.Data.Contract.Models.Content.Poll;

    internal class PollAnswer : _pollAnswer
    {
        public override byte Id { get; set; }
        public override string Text { get; set; }
        
        public ContentPoll ContentPoll { get; set; }
        public long ContentPollId { get; set; }
    }

    internal class PollAnswerConfiguration : EntityTypeConfiguration<PollAnswer>
    {
        public PollAnswerConfiguration()
        {
            ToTable("Content_Poll_Answer", "dbo");

            this.Property(cpa => cpa.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(cpa => cpa.ContentPoll)
                .WithMany(cp => (ICollection<PollAnswer>) cp.Answers)
                .HasForeignKey(c => c.ContentPollId);
        }
    }
}
