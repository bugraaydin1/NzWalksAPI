using NzWalksAPI.Domain.Domain;

namespace NzWalksAPI.Domain.DTO
{
    public class DifficultyDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}