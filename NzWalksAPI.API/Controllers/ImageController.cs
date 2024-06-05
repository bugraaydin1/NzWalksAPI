using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NzWalksAPI.Domain.DTO;
using NzWalksAPI.Repositories.Repositories;
using NzWalksAPI.Domain.Domain;
using NzWalksAPI.Domain.DTO;

namespace NzWalksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController
    (
        IImageRepository imageRepository,
        IMapper mapper
    ) : ControllerBase
    {
        private readonly IImageRepository imageRepository = imageRepository;
        private readonly IMapper mapper = mapper;

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<ActionResult<string>> GetAll()
        {
            var imagesModel = await imageRepository.GetAllAsync();
            return Ok(mapper.Map<List<ImageDto>>(imagesModel));
        }

        [HttpPost("upload")]
        [Authorize(Roles = "Writer")]
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