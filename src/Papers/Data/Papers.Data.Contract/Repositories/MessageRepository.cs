namespace Papers.Data.Contract.Repositories
{
    using Papers.Common.Contract.Enums;
    using Papers.Data.Contract.Models;

    public interface IMessageRepository
    {
        SendResult Send(_user from, _chat chat, _message message);

        // TODO testing
        _message GenerateMessage();
    }
}
