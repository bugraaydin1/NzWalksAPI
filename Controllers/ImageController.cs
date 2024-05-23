using Microsoft.AspNetCore.Mvc;
using NzWalksAPI.Repositories;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController(IImageRepository imageRepository) : ControllerBase
    {
        private readonly IImageRepository imageRepository = imageRepository;

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequstDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageModel = new Image()
                {
                    Name = request.Name,
                    File = request.File,
                    Extension = Path.GetExtension(request.File.FileName),
                    Size = request.File.Length,
                };
                await imageRepository.Upload(imageModel);

                return Ok(imageModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequstDto request)
        {
            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 1048760)
            {
                ModelState.AddModelError("file", "File size exceeded.");
            }
        }
    }
}