using NzWalksAPI.Domain.Domain;
using NzWalksAPI.Domain.DTO;

namespace NzWalksAPI.Data.Repositories;

public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync(
        string? filterOn = null, string? filter = null,
        string? sortBy = null, bool isAscending = true,
        int page = 1, int pageSize = 50
    );
    Task<Region?> GetByIdAsync(Guid id);
    Task<Region> CreateAsync(Region region);
    Task<Region?> UpdateAsync(Guid id, Region region);
    Task<Region?> DeleteAsync(Guid id);
}
