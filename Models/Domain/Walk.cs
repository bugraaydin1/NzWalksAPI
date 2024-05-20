namespace NZWalksAPI.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required double Length { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Navigation property:
        public required Difficulty Difficulty { get; set; }
        public required Region Region { get; set; }
    }
}