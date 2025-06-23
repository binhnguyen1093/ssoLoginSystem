namespace LoginSystem.DTOs
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; } = "bearer";
    }
}
