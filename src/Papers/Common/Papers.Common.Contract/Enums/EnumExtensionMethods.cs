using System;

namespace Papers.Common.Contract.Enums
{
    public static class EnumExtensionMethods
    {
        public static byte ToByteState(this UserState state)
        {
            switch (state)
            {
                case UserState.New:
                    return 2;
                case UserState.NeedVerification:
                    return 4;
                case UserState.Registered:
                    return 6;
                case UserState.Removed:
                    return 8;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public static UserState ToEnumState(this byte state)
        {
            return (UserState)Enum.Parse(typeof(UserState), state.ToString());
        }
    }
}
