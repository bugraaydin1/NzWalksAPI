using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class LoginResponseDto
    {
        public required string Username { get; set; }
        public required string JwtToken { get; set; }
    }
}