using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Models.DTO
{
    public class DifficultyDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}