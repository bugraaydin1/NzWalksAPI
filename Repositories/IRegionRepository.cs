using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Repositories;

public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync(
        string? filterOn = null,
        string? filter = null,
        string? sortBy = null,
        bool isAscending = true
    );
    Task<Region?> GetByIdAsync(Guid id);
    Task<Region> CreateAsync(Region region);
    Task<Region?> UpdateAsync(Guid id, Region region);
    Task<Region?> DeleteAsync(Guid id);
}
