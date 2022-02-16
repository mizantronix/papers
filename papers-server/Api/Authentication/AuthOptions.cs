namespace Papers.Api.Authentication
{
    using System.Text;

    using Microsoft.IdentityModel.Tokens;

    public class AuthOptions
    {
        public static string Issuer { get; private set; }
        public static string Audience { get; private set; }
        public static string SecretKey { get; private set; }
        public static int TokenLifetime { get; private set; }
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }

        public static void Configure(string issuer, string audience, string secretKey, int tokenLifetime)
        {
            Issuer = issuer;
            Audience = audience;
            SecretKey = secretKey;
            TokenLifetime = tokenLifetime;
        }

        public static TokenValidationParameters GetTokenValidationParams()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = AuthOptions.Audience,

                ValidateLifetime = false,

                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };
        }
    }
}
