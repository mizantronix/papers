namespace Papers.Data.MsSql.Models.Content.Poll
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Papers.Data.Contract.Models;
    using Papers.Data.Contract.Models.Content.Poll;

    internal class UserPollAnswer : _content_poll_result
    {
        public override _user User { get; set; }
        public long UserId { get; set; }

        public override _content_poll Poll { get; set; }
        public long PollId { get; set; }
    }

    internal class UserPollAnswerConfiguration : EntityTypeConfiguration<UserPollAnswer>
    {
        public UserPollAnswerConfiguration()
        {
            ToTable("Content_Poll_UserAnswer_Xref", "dbo");
            
            this.HasRequired(ua => (ContentPoll)ua.Poll)
                .WithMany(cp => (ICollection<UserPollAnswer>) cp.UserPollAnswers)
                .HasForeignKey(c => c.PollId);

            this.HasRequired(ua => (User) ua.User)
                .WithMany(u => (ICollection<UserPollAnswer>) u.UserPollAnswers)
                .HasForeignKey(c => c.UserId);
        }
    }
}
