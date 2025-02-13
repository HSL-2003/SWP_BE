using MediatR;
using SWP391_BE.DTOs.Auth.DTO;
namespace SWP391_BE.DTOs.Auth.LoginAdmin
{
    public class LoginAdmin : IRequest<AuthResponse>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
