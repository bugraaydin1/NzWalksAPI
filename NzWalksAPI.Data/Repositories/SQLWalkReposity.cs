using Microsoft.EntityFrameworkCore;
using NzWalksAPI.Data;
using NzWalksAPI.Domain.Domain;
// using NzWalksAPI.Domain.DTO;
// using NzWalksAPI.Data.Repositories;

namespace NzWalksAPI.Data.Repositories
{
    public class SQLWalkRepository(NZWalksDbContext dbContext) : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext = dbContext;

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(r => r.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            _dbContext.Remove(existingWalk);
            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(
            string? filterOn = null, string? filter = null,
            string? sortBy = null, bool isAscending = true,
            int page = 1, int pageSize = 50)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filter))
            {
                switch (filterOn.ToLower())
                {
                    case "name":
                        walks = walks.Where(w => w.Name.Contains(filter));
                        break;
                    case "description":
                        walks = walks.Where(w => w.Description.Contains(filter));
                        break;
                    case "length":
                        if (double.TryParse(filter, out double length))
                        {
                            walks = walks.Where(w => w.Length == length);
                        }
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
                        walks = isAscending ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
                        break;
                    case "description":
                        walks = isAscending ? walks.OrderBy(w => w.Description) : walks.OrderByDescending(w => w.Description);
                        break;
                    case "length":
                        walks = isAscending ? walks.OrderBy(w => w.Length) : walks.OrderByDescending(w => w.Length);
                        break;
                    default:
                        break;
                }
            }

            return await walks.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = _dbContext.Walks.FirstOrDefault(r => r.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.Description = walk.Description;
            existingWalk.ImageUrl = walk.ImageUrl;

            await _dbContext.SaveChangesAsync();

            return existingWalk;
        }

    }
}