using System.ComponentModel.DataAnnotations;

namespace NzWalksAPI.Domain.DTO
{
    public class LoginResponseDto
    {
        public required string Username { get; set; }
        public required string JwtToken { get; set; }
    }
}