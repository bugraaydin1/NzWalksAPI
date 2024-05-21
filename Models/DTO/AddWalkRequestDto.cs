namespace NZWalksAPI.Models.DTO
{
    public class AddWalkRequestDto
    {
        public required string Name { get; set; }
        public required double Length { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}