using NzWalksAPI.Domain.Domain;

namespace NzWalksAPI.Data.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
        Task<List<Image>> GetAllAsync();
    }
}