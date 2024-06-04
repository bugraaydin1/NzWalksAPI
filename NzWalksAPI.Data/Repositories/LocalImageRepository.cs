using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NzWalksAPI.Domain.Domain;
using NzWalksAPI.Data;

namespace NzWalksAPI.Data.Repositories
{
    public class LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext) : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        private readonly NZWalksDbContext dbContext = dbContext;

        public async Task<List<Image>> GetAllAsync()
        {
            return await dbContext.Images.ToListAsync();
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.Name}{image.Extension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.Name}{image.Extension}";
            image.Path = urlFilePath;

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
    }
}