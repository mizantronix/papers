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

    internal class MessageManager : IMessageManager
    {
        public SendResult SendMessage(long chatId, Message message)
        {
            throw new System.NotImplementedException();
        }

        public SendResult EditMessage(long messageId, Message message)
        {
            throw new System.NotImplementedException();
        }

        public SendResult RemoveMessage(long chatId)
        {
            throw new System.NotImplementedException();
        }

        public SendResult ForwardMessages(long chatId, IEnumerable<Message> missing_name)
        {
            throw new System.NotImplementedException();
        }
    }
}
