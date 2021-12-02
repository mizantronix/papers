namespace Papers.Common.Enums
{
    public enum UserState
    {
        // Registration begun, need to finish registration
        New = 2,

        // Registration finished, need to verify phone number
        NeedVerification = 4,

        // Phone number verified
        Registered = 6,

        // User removed, need verify to be restored
        Removed = 8,
    }
}
