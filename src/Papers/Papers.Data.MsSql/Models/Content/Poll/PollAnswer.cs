namespace Papers.Data.MsSql.Models.Content.Poll
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    
    public class PollAnswer
    {
        public long Id { get; set; }
        public string Text { get; set; }
        
        public ContentPoll ContentPoll { get; set; }
        public long ContentPollId { get; set; }
    }

    internal class PollAnswerConfiguration : EntityTypeConfiguration<PollAnswer>
    {
        public PollAnswerConfiguration()
        {
            ToTable("Content_Poll_Answer", "dbo");
            this.HasKey(cpa => cpa.Id);
            this.HasRequired(cpa => cpa.ContentPoll)
                .WithMany(cp => (ICollection<PollAnswer>) cp.Answers)
                .HasForeignKey(c => c.ContentPollId);
        }
    }
}
