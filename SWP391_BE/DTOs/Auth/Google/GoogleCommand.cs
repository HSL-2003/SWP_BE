using MediatR;
using SWP391_BE.DTOs.Auth.DTO;

namespace SWP391_BE.DTOs.Auth.Google
{
    public class GoogleCommand : IRequest<AuthResponse>
    {
        public required string GoogleID { get; set; }
        public required string Name { get; set; }
        public required string ImageUrl { get; set; }
        public required string Email { get; set; }
    }
}
