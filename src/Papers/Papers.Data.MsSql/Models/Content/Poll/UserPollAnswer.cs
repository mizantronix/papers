using System.ComponentModel.DataAnnotations.Schema;

namespace Papers.Data.MsSql.Models.Content.Poll
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.ModelConfiguration;
    
    public class UserPollAnswer
    {
        public long Id { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }

        public ContentPoll Poll { get; set; }
        public long PollId { get; set; }
    }

    internal class UserPollAnswerConfiguration : EntityTypeConfiguration<UserPollAnswer>
    {
        public UserPollAnswerConfiguration()
        {
            ToTable("Content_Poll_UserAnswer_Xref", "dbo");
            this.HasKey(ua => ua.Id);
            this.HasRequired(ua => (ContentPoll)ua.Poll)
                .WithMany(cp => (ICollection<UserPollAnswer>) cp.UserPollAnswers)
                .HasForeignKey(c => c.PollId);

            this.HasRequired(ua => (User) ua.User)
                .WithMany(u => (ICollection<UserPollAnswer>) u.UserPollAnswers)
                .HasForeignKey(c => c.UserId);
        }
    }
}
