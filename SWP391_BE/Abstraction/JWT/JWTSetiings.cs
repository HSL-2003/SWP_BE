namespace SWP391_BE.Abstraction.JWT
{
    public class JWTSetiings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
    }
}
