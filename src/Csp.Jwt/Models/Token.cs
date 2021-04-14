namespace Csp.Jwt.Models
{
    public class Token
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; } = "bearer";

        public long ExpiresIn { get; set; }

        public string RefreshToken { get; set; }


        public Token(string token, int expires)
        {
            AccessToken = token;
            ExpiresIn = expires;
        }
    }
}
