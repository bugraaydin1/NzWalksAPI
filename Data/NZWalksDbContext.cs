using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    public class NZWalksDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        // explicit construtor (primary used instead):
        // public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)

        public required DbSet<Difficulty> Difficulties { get; set; }
        public required DbSet<Region> Regions { get; set; }
        public required DbSet<Walk> Walks { get; set; }
    }
}