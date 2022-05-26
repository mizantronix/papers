namespace Papers.Common.Enums
{
    using System;

    public static class EnumExtensionMethods
    {
        public static byte ToByteState(this UserState state)
        {
            return state switch
            {
                UserState.New => 2,
                UserState.NeedVerification => 4,
                UserState.Registered => 6,
                UserState.Removed => 8,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }

        public static UserState ToEnumState(this byte state)
        {
            return (UserState)Enum.Parse(typeof(UserState), state.ToString());
        }

        public static MessageContentType ToEnumState(this int contentType)
        {
            return (MessageContentType)Enum.Parse(typeof(MessageContentType), contentType.ToString());
        }

        public static int ToIntContentType(this MessageContentType contentType)
        {
            return contentType switch
            {
                MessageContentType.Text => 1,
                MessageContentType.Picture => 51,
                MessageContentType.Poll => 101,
                _ => throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null)
            };
        }
    }
}
