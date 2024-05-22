using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [Range(0, 50)]
        public required double Length { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public string? ImageUrl { get; set; }


        [Required]
        public required Guid DifficultyId { get; set; }
        [Required]
        public required Guid RegionId { get; set; }
    }
}