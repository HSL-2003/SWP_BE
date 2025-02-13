using MediatR;
using Microsoft.IdentityModel.Tokens;
using Repo;
using SWP391_BE.Abstraction.JWT;
using SWP391_BE.Abstraction.Logging;
using SWP391_BE.Abstraction.Utility;
using SWP391_BE.DTOs.Auth.DTO;
using SWP391_BE.DTOs.Auth.Register;
using SWP391_BE.Exceptions;

namespace SWP391_BE.DTOs.Auth.LoginUser
{
    public class LoginUserHandle : IRequestHandler<LoginUser, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<RegisterHandler> _logger;
        private readonly IUtilityService _utilityService;
        private readonly IJWT _jwt;

        public LoginUserHandle(IUserRepository userRepository, IAppLogger<RegisterHandler> logger, IUtilityService utilityService, IJWT jWT)
        {
            _userRepository = userRepository;
            _logger = logger;
            _utilityService = utilityService;
            _jwt = jWT;
        }
        public async Task<AuthResponse> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => (x.PhoneNumber == request.UserName || x.Email == request.UserName), cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.UserName);
            }
            if (!_utilityService.Verify(request.Password, user.PasswordHash))
                throw new BadRequestException("Password incorrect!");
            if (user.RoleId != 1 && user.RoleId != 2)
            {
                throw new BadRequestException("invalid request");
            }
            _logger.LogInformation($"Login Account: {user.Email}");

            var jwtResult = _jwt.CreateTokenJWT(user);

            return new AuthResponse()
            {
                UserID = user.UserId.ToString(),
                Token = jwtResult.Token,
                Expiration = jwtResult.Expiration,
                Role = user.Role?.ToString() ?? "Unknown",
                IsVerification = user.IsVerification,
            };
        }
    }
}
