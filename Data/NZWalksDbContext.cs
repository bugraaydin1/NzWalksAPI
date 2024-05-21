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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed DB with difficulties
            var difficulties = new List<Difficulty>()
            {
                new() { Id = new Guid("afcc9581-dcb9-4096-916a-fbed178c3821"), Name = "Easy"},
                new() { Id = new Guid("f57e2c5d-2b32-490a-bfb2-d5a4b0d113cb"), Name = "Medium"},
                new() { Id = new Guid("cfd30386-d2c9-4fd1-8646-51b02bdb3c17"), Name = "Hard"},
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed DB with regions 
            var regions = new List<Region>()
            {
                new()  {
                    Id = Guid.Parse("0c4d772e-27fa-4247-c199-08dc7985bde5"),
                    Code = "WLG",
                    Name = "Wellington Region",
                    ImageUrl = "https://fastly.picsum.photos/id/1001/800/600.jpg?hmac=SYMSTZ_jGTLCAYO7vGHOck2TiSy2qjo6OPHwBGNem0I",
                },
                new()  {
                    Id = Guid.Parse("63e99d9b-a915-4d21-1025-08dc7989d7f2"),
                    Code = "QLS",
                    Name = "Queenstown Region",
                    ImageUrl = "https://fastly.picsum.photos/id/176/800/600.jpg?hmac=dGLT-aXDtvYjsZo8E4Vp7osGVQ_z0Gw0BomVl52uU5o"
                },
                new()  {
                    Id = Guid.Parse("b517a10d-2e08-4031-9c6b-b26e77560c36"),
                    Code = "AKL",
                    Name = "Auckland",
                    ImageUrl = "https://fastly.picsum.photos/id/337/800/600.jpg?hmac=Gx8pjhwU87sYnp2K9YBi3hvR78dBeAUbrVF6KJCwGJA"
                },
                new()  {
                    Id = Guid.Parse("3da94be2-f663-4e18-8843-ede475618c77"),
                    Code = "STL",
                    Name = "Southland",
                    ImageUrl = null
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);

        }

    }
}