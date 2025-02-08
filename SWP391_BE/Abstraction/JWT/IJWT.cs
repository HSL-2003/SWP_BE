using Data.Models;

namespace SWP391_BE.Abstraction.JWT
{
    public interface IJWT
    {
        public JWTResponse CreateTokenJWT(User user);
        public Guid UserID { get; }
    }
}
