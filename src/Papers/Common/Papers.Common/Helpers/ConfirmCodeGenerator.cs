namespace Papers.Common.Helpers
{
    using System.Linq;

    public static class ConfirmCodeGenerator
    {
        // TODO test generator
        public static string GenerateConfirmCode(long id, string phoneNumber, string login)
        {
            int n1;
            unchecked
            {
                n1 = id.GetHashCode() * 16 + 17;
                if (n1 < 0)
                {
                    n1 *= -1;
                }
            }

            int n2;
            unchecked
            {
                n2 = phoneNumber.GetHashCode() * 7 + 41;
                if (n2 < 0)
                {
                    n2 *= -1;
                }
            }

            int n3;
            unchecked
            {
                n3 = login.GetHashCode() * 111 + 11;
                if (n3 < 0)
                {
                    n3 *= -1;
                }
            }

            return
                $"{n1.ToString()[0] - 48}" +
                $"{n1.ToString()[1] - 48}" +
                $"{n2.ToString()[0] - 48}" +
                $"{n2.ToString()[1] - 48}" +
                $"{n3.ToString()[0] - 48}" +
                $"{n3.ToString()[1] - 48}";
        }
    }
}
