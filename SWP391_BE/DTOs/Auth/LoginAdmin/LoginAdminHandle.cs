using MediatR;
using Microsoft.IdentityModel.Tokens;
using Repo;
using SWP391_BE.Abstraction.JWT;
using SWP391_BE.Abstraction.Logging;
using SWP391_BE.Abstraction.Utility;
using SWP391_BE.DTOs.Auth;
using SWP391_BE.DTOs.Auth.DTO;
using SWP391_BE.DTOs.Auth.Register;
using SWP391_BE.Exceptions;

namespace SWP391_BE.DTOs.Auth.LoginAdmin
{
    public class LoginAdminHandle : IRequestHandler<LoginAdmin, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<RegisterHandler> _logger;
        private readonly IUtilityService _utilityService;
        private readonly IJWT _jwt;

        public LoginAdminHandle(IUserRepository userRepository, IAppLogger<RegisterHandler> logger, IUtilityService utilityService, IJWT jWT)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _utilityService = utilityService ?? throw new ArgumentNullException(nameof(utilityService));
            _jwt = jWT ?? throw new ArgumentNullException(nameof(jWT));
        }

        public async Task<AuthResponse> Handle(LoginAdmin request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var user = await _userRepository.FirstOrDefaultAsync(
                x => (x.PhoneNumber == request.UserName || x.Email == request.UserName), 
                cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.UserName);
            }

            // Check if user has admin role (3 or 4)
            if (user.RoleId != 3 && user.RoleId != 4)
            {
                throw new BadRequestException("User does not have admin privileges");
            }

            // Verify password
            if (!_utilityService.Verify(request.Password, user.PasswordHash))
            {
                throw new BadRequestException("Password incorrect!");
            }

            _logger.LogInformation($"Login Admin Account: {user.Email}");

            var jwtResult = _jwt.CreateTokenJWT(user);

            return new AuthResponse
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
