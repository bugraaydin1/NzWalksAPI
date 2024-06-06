namespace NzWalksAPI.Domain.DTO
{
    public class ImageDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Path { get; set; }
    }
}