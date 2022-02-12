namespace Papers.Common.Helpers
{
    using System;
    using System.Linq;

    public static class ConfirmCodeGenerator
    {
        public static string GenerateConfirmCode(long id, string phoneNumber, string login)
        {
            var loginLength = login.Length;

            int n1;
            unchecked
            {
                n1 = Convert.ToInt32((Math.Log2(loginLength) + login.Count(c => c is 'a' or 'o' or 'i')) * 111.11) ;
            }

            int n2;
            unchecked
            {
                n2 = 
                    17 + 
                    Convert.ToInt32(phoneNumber[..5]) - 
                    Convert.ToInt32(phoneNumber.Substring(5, 5));
                if (n2 < 0)
                {
                    n2 *= -1;
                }
            }

            int n3;
            
            // TODO bad function
            unchecked
            {
                n3 = (Convert.ToInt32(id) * Convert.ToInt32(id + 43) + 13 + Convert.ToInt32(id) % 7);
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
