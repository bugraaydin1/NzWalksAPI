using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NzWalksAPI.Controllers.Base;
using NzWalksAPI.CustomActionFilters;
using NzWalksAPI.Domain.Domain;
using NzWalksAPI.Domain.DTO;
using NzWalksAPI.Data.Repositories;

namespace NzWalksAPI.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    public class WalksController
    (
      IWalkRepository walkRepository,
      IMapper mapper
    ) : BaseController
    {
        private readonly IWalkRepository walkRepository = walkRepository;
        private readonly IMapper mapper = mapper;

        [HttpGet]
        [MapToApiVersion("1")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn, [FromQuery] string? filter,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var walkModel = await walkRepository.GetAllAsync(filterOn, filter, sortBy, isAscending ?? true, page, pageSize);

            return Ok(mapper.Map<List<WalkDto>>(walkModel));
        }
        [HttpGet]
        [MapToApiVersion("2")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllV2(
            [FromQuery] string? filterOn, [FromQuery] string? filter,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var walkModel = await walkRepository.GetAllAsync(filterOn, filter, sortBy, isAscending ?? true, page, pageSize);

            return Ok(mapper.Map<List<WalkDtoV2>>(walkModel));
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkModel = await walkRepository.GetByIdAsync(id);

            if (walkModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkModel));
        }
        [HttpGet("{id}")]
        [MapToApiVersion("2")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetByIdV2(Guid id)
        {
            var walkModel = await walkRepository.GetByIdAsync(id);

            if (walkModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDtoV2>(walkModel));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Create(AddWalkRequestDto addWalkRequestDto)
        {
            var walkModel = mapper.Map<Walk>(addWalkRequestDto);

            walkModel = await walkRepository.CreateAsync(walkModel);
            var walkDto = mapper.Map<WalkDto>(walkModel);

            return CreatedAtAction(nameof(GetById), new { walkDto.Id }, walkDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, AddWalkRequestDto addWalkRequestDto)
        {
            var walkModel = mapper.Map<Walk>(addWalkRequestDto);

            walkModel = await walkRepository.UpdateAsync(id, walkModel);

            if (walkModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkModel));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walkModel = await walkRepository.DeleteAsync(id);
            if (walkModel == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}