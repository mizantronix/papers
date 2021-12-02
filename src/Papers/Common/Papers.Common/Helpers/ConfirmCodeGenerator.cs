namespace Papers.Common.Helpers
{
    using System.Linq;

    public static class ConfirmCodeGenerator
    {
        // TODO test generator
        public static string GenerateConfirmCode(long id, string phoneNumber, string login)
        {
            var n1 = phoneNumber.Count(c => c == '6');
            if (n1 == 10)
            {
                n1 = 9;
            }

            var n2 = phoneNumber.Count(c => c == '2');
            if (n2 == 10)
            {
                n2 = 3;
            }

            var n3 = (10 % (id.ToString()[0] - 48));
            var n4 = (10 / (id.ToString()[0] - 48));
            if (n4 == 10)
            {
                n4 = 4;
            }

            var n5 = login.Count(c => c is 's' or 'c' or '1');
            n5 = n5 switch
            {
                < 1 => 2,
                >= 10 => 6,
                _ => n5
            };

            var n6 = login.Count(c => c is 'a' or 'y' or 'i');
            n5 = n6 switch
            {
                < 1 => 2,
                >= 10 => 6,
                _ => n6
            };

            return $"{n1}{n2}{n3}{n4}{n5}{n6}";
        }
    }
}
