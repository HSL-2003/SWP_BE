using MediatR;
using Microsoft.IdentityModel.Tokens;
using Repo;
using SWP391_BE.Abstraction.Logging;
using SWP391_BE.Abstraction.Utility;
using SWP391_BE.Exceptions;

namespace SWP391_BE.DTOs.Auth.Register
{
    public class RegisterHandler : IRequestHandler<Register, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<RegisterHandler> _logger;
        private readonly IUtilityService _utilityService;

        public RegisterHandler(IUserRepository userRepository, IAppLogger<RegisterHandler> logger, IUtilityService utilityService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _utilityService = utilityService;
        }

        public async Task<string> Handle(Register request, CancellationToken cancellationToken)
        {
            var validator = new RegisterValidation(_userRepository);
            var validationResult =await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Register User Request", validationResult);
            string generationCode = _utilityService.GenerationCode();
            Data.Models.User user = new()
            {
                
                Username = request.Username.Trim(),
                FullName = request.Username.Trim(),
                Password = request.Password,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                
            };
            await _userRepository.AddAsync(user);
            return "Create new user successfully";
        }
    }
}
