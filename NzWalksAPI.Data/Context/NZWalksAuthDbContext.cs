using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NzWalksAPI.Domain.Domain;

namespace NzWalksAPI.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "e2b039f9-e5f4-4790-88f4-abfc5ffdd026";
            var writerRoleId = "c1c5af31-4100-4cee-a011-50b990df3346";

            var roles = new List<IdentityRole>
            {
                new(){ Id= readerRoleId, Name="Reader", ConcurrencyStamp=readerRoleId, NormalizedName="Reader".ToUpper()},
                new(){ Id= writerRoleId, Name="Writer", ConcurrencyStamp=writerRoleId, NormalizedName="Writer".ToUpper()},
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}