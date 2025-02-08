using MediatR;
using SWP391_BE.DTOs.Auth.DTO;

namespace SWP391_BE.DTOs.Auth.LoginUser
{
    public class LoginUser : IRequest<AuthResponse>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
