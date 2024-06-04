using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalksAPI.Controllers.Base;
using NzWalksAPI.CustomActionFilters;
using NzWalksAPI.Data;
using NzWalksAPI.Domain.Domain;
using NzWalksAPI.Domain.DTO;
using NzWalksAPI.Data.Repositories;

namespace NzWalksAPI.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    public class RegionsController
   (
       IRegionRepository regionRepository,
       IMapper mapper,
       ILogger<RegionsController> logger
   ) : BaseController
    {
        private readonly IRegionRepository _regionRepository = regionRepository;
        private readonly IMapper mapper = mapper;
        private readonly ILogger<RegionsController> logger = logger;

        [HttpGet]
        [MapToApiVersion("1")]
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
        [HttpGet]
        [MapToApiVersion("2")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllV2(
                [FromQuery] string? filterOn, [FromQuery] string? filter,
                [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
                [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            logger.LogInformation("Request start. GetAll Regions");

            var regionsModel = await _regionRepository.GetAllAsync(filterOn, filter, sortBy, isAscending ?? true, page, pageSize);
            var regionsDto = mapper.Map<List<RegionDtoV2>>(regionsModel);

            logger.LogInformation($"Request finished. GetAll Regions with data: {JsonSerializer.Serialize(regionsModel)}");

            return Ok(regionsDto);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1")]
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
        [HttpGet("{id}")]
        [MapToApiVersion("2")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetByIdV2(Guid id)
        {
            logger.LogInformation("Request start. Get Region with id:{id}", id);
            var regionModel = await _regionRepository.GetByIdAsync(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            logger.LogInformation($"Request finished. Get Region: {JsonSerializer.Serialize(regionModel)}");

            return Ok(mapper.Map<RegionDtoV2>(regionModel));
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