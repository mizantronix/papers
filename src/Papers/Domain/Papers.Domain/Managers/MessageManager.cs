namespace Papers.Domain.Managers
{
    using System.Collections.Generic;

    using Papers.Common.Contract.Enums;
    using Papers.Common.Contract.Filters;
    using Papers.Domain.Models.Message;
    
    public interface IMessageManager
    {
        public SendResult SendMessage(long chatId, Message message);

        public SendResult EditMessage(long messageId, Message message);

        public SendResult RemoveMessage(long chatId);

        public SendResult ForwardMessages(long chatId, IEnumerable<Message> messages);

        public Message GetMessage(long messageId);

        public IEnumerable<Message> GetMessages(MessageFilter filter);
    }
}
