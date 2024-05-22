using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code should be min 3 characters")]
        [MaxLength(3, ErrorMessage = "Code should be max 3 characters")]
        public required string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name should be max 100 characters")]
        public required string Name { get; set; }

        public string? ImageUrl { get; set; }
    }
}