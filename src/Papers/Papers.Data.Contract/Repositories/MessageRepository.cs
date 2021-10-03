namespace Papers.Data.Contract.Repositories
{
    using System.Collections.Generic;

    using Papers.Common.Contract.Filters;
    using Papers.Data.Contract.Models;

    public interface IMessageRepository
    {
        public IEnumerable<Message> GetMessages(MessageFilter filter);

        public Message GetMessage(long messageId);

        public long AddMessage(Message message, long chatId);

        public long EditMessage(long messageId, Message message);

        public void RemoveMessage(long messageId);
    }
}
