using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace NzWalksAPI.Domain.DTO
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