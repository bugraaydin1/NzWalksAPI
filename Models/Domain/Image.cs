using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPI.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public required IFormFile File { get; set; }
        public required string Name { get; set; }
        public required long Size { get; set; }
        public required string Extension { get; set; }
        public string? Description { get; set; }
        public string Path { get; set; }
    }
}