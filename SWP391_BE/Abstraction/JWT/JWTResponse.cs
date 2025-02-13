namespace SWP391_BE.Abstraction.JWT
{
    public class JWTResponse
    {
        public required string Token { get; set; }
        public required DateTime Expiration { get; set; }
    }
}
