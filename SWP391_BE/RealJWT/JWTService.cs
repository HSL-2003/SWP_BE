using Data.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SWP391_BE.Abstraction.JWT;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace SWP391_BE.RealJWT
{
    public class JWTService : IJWT
    {
        private readonly JWTSetiings _jwtSetiing;
        private readonly IHttpContextAccessor _contextAccessor;
        public JWTService(IOptions<JWTSetiings> jwtSettings, IHttpContextAccessor contextAccessor)
        {
            _jwtSetiing = jwtSettings.Value;
            _contextAccessor = contextAccessor;
        }
        public Guid UserID
        {
            get
            {
                var userId = _contextAccessor?.HttpContext?.User.FindFirstValue("UserId");
                return Guid.TryParse(userId, out var result) ? result : Guid.Empty;
            }
        }

        public JWTResponse CreateTokenJWT(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSetiing.Key);
            var securityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]{
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var timeExpiration = DateTime.Now.AddMinutes(_jwtSetiing.DurationMinutes);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSetiing.Issuer,
                audience: _jwtSetiing.Audience,
                claims: claims,
                expires: timeExpiration,
                signingCredentials: signingCredentials
            );

            return new JWTResponse
            {
                Token = tokenHandler.WriteToken(jwtSecurityToken),
                Expiration = timeExpiration,
            };
        }

        
    }
}
