namespace NZWalksAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        public string Code { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}