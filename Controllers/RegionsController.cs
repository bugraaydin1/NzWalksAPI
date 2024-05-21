using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository) : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext = dbContext;
        private readonly IRegionRepository _regionRepository = regionRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsModel = await _regionRepository.GetAllAsync();
            var regionsDto = new List<RegionDto>();

            foreach (var regionModel in regionsModel)
            {
                regionsDto.Add(
                    new()
                    {
                        Id = regionModel.Id,
                        Name = regionModel.Name,
                        Code = regionModel.Code,
                        ImageUrl = regionModel.ImageUrl
                    });
            }

            return Ok(regionsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // var region = _dbContext.Regions.Find(id);
            var regionModel = await _regionRepository.GetByIdAsync(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = regionModel.Id,
                Name = regionModel.Name,
                Code = regionModel.Code,
                ImageUrl = regionModel.ImageUrl
            };

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionRequestDto)
        {

            var regionModel = new Region
            {
                Code = regionRequestDto.Code,
                Name = regionRequestDto.Name,
                ImageUrl = regionRequestDto.ImageUrl,
            };

            regionModel = await _regionRepository.CreateAsync(regionModel);

            var regionDto = new RegionDto()
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                ImageUrl = regionModel.ImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, AddRegionRequestDto addRegionRequestDto)
        {
            var regionModel = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                ImageUrl = addRegionRequestDto.ImageUrl,
            };

            regionModel = await _regionRepository.UpdateAsync(id, regionModel);

            if (regionModel == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                ImageUrl = regionModel.ImageUrl,
            };

            return Ok(regionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exists = await _regionRepository.DeleteAsync(id);

            if (exists == null)
            {
                return NotFound();
            };

            return Ok();
        }

    }
}