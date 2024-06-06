using NzWalksAPI.Domain.Domain;

namespace NzWalksAPI.Domain.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required double Length { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }

    public class WalkDtoV2
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required double Length { get; set; }
        public string? ImageUrl { get; set; }
    }
}