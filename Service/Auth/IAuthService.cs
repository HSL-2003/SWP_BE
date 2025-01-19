using SWP391_BE.Dtos.Auth;

namespace Service.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(UserRegisterDto model);
        Task<AuthResponseDto> LoginAsync(LoginDto model);
        Task<UserDto> GetUserByIdAsync(int userId);
    }
} 