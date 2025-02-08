using MediatR;
using SWP391_BE.DTOs.Auth.DTO;

namespace SWP391_BE.DTOs.Auth.LoginUser
{
    public class LoginUserHandle : IRequestHandler<LoginUser, AuthResponse>
    {
    }
}
