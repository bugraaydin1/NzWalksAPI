using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController
    (
        IRegionRepository regionRepository,
        IMapper mapper,
        ILogger<RegionsController> logger
    ) : ControllerBase
    {
        private readonly IRegionRepository _regionRepository = regionRepository;
        private readonly IMapper mapper = mapper;
        private readonly ILogger<RegionsController> logger = logger;

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll(
                [FromQuery] string? filterOn, [FromQuery] string? filter,
                [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
                [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            logger.LogInformation("Request start. GetAll Regions");

            var regionsModel = await _regionRepository.GetAllAsync(filterOn, filter, sortBy, isAscending ?? true, page, pageSize);
            var regionsDto = mapper.Map<List<RegionDto>>(regionsModel);

            logger.LogInformation($"Request finished. GetAll Regions with data: {JsonSerializer.Serialize(regionsModel)}");

            return Ok(regionsDto);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            logger.LogInformation("Request start. Get Region with id:{id}", id);
            var regionModel = await _regionRepository.GetByIdAsync(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            logger.LogInformation($"Request finished. Get Region: {JsonSerializer.Serialize(regionModel)}");

            return Ok(mapper.Map<RegionDto>(regionModel));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionRequestDto)
        {
            var regionModel = mapper.Map<Region>(regionRequestDto);

            regionModel = await _regionRepository.CreateAsync(regionModel);

            var regionDto = mapper.Map<RegionDto>(regionModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, AddRegionRequestDto addRegionRequestDto)
        {
            var regionModel = mapper.Map<Region>(addRegionRequestDto);

            regionModel = await _regionRepository.UpdateAsync(id, regionModel);

            if (regionModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionModel));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
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