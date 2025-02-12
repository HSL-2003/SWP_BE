using MediatR;

namespace SWP391_BE.DTOs.Auth.Register
{
    public class Register : IRequest<string>
    {
        public required string Username { get; set; } 

        public required string Password { get; set; } 

        public required string? FullName { get; set; }

        public required string? Email { get; set; }

        public required string? PhoneNumber { get; set; }

    }
}
