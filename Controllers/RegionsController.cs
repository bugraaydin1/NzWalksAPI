using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController(NZWalksDbContext dbContext) : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext = dbContext;

        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsModel = _dbContext.Regions.ToList();
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
        public IActionResult GetById(Guid id)
        {
            // var region = _dbContext.Regions.Find(id);
            var regionModel = _dbContext.Regions.FirstOrDefault(r => r.Id == id);

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
        public IActionResult Create([FromBody] AddRegionRequestDto regionRequestDto)
        {

            var regionModel = new Region
            {
                Code = regionRequestDto.Code,
                Name = regionRequestDto.Name,
                ImageUrl = regionRequestDto.ImageUrl,
            };

            _dbContext.Add(regionModel);
            _dbContext.SaveChanges();

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
        public IActionResult Update(Guid id, AddRegionRequestDto addRegionRequestDto)
        {

            var regionModel = _dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (regionModel == null)
            {
                return NotFound();
            }

            regionModel.Code = addRegionRequestDto.Code;
            regionModel.Name = addRegionRequestDto.Name;
            regionModel.ImageUrl = addRegionRequestDto.ImageUrl;

            _dbContext.SaveChanges();

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
        public IActionResult Delete(Guid id)
        {
            var regionModel = _dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (regionModel == null)
            {
                return NotFound();
            };

            _dbContext.Remove(regionModel);
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}