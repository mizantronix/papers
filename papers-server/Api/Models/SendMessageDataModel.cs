namespace Papers.Api.Models
{
    using Papers.Domain.Models.Message;

    // TODO remove model, user chatId and GetCurrentUser()
    public class SendMessageDataModel
    {
        public long ChatId { get; set; }

        public Message Message { get; set; }
    }
}
