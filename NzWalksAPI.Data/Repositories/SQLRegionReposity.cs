using Microsoft.EntityFrameworkCore;
using NzWalksAPI.Data;
using NzWalksAPI.Domain.Domain;
using NzWalksAPI.Domain.DTO;
using NzWalksAPI.Data.Repositories;

namespace NzWalksAPI.Data.Repositories
{
    public class SQLRegionRepository(NZWalksDbContext dbContext) : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext = dbContext;

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            _dbContext.Remove(existingRegion);
            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync(
            string? filterOn, string? filter,
            string? sortBy, bool isAscending = true,
            int page = 1, int pageSize = 50)
        {
            var regions = _dbContext.Regions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filter))
            {
                switch (filterOn.ToLower())
                {
                    case "name":
                        regions = regions.Where(r => r.Name.Contains(filter));
                        break;
                    case "code":
                        regions = regions.Where(r => r.Code.Contains(filter));
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        regions = isAscending ? regions.OrderBy(r => r.Name) : regions.OrderByDescending(r => r.Name);
                        break;
                    case "code":
                        regions = isAscending ? regions.OrderBy(r => r.Code) : regions.OrderByDescending(r => r.Code);
                        break;
                    default:
                        break;
                }
            }

            return await regions.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = _dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.ImageUrl = region.ImageUrl;

            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}