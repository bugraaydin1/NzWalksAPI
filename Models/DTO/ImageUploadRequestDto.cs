using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class ImageUploadRequstDto
    {
        [Required]
        public required IFormFile File { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}