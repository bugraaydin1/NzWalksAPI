using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WalksController
    (
      IWalkRepository walkRepository,
      IMapper mapper
    ) : ControllerBase
    {
        private readonly IWalkRepository walkRepository = walkRepository;
        private readonly IMapper mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walkModel = await walkRepository.GetAllAsync();

            return Ok(mapper.Map<List<WalkDto>>(walkModel));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkModel = await walkRepository.GetByIdAsync(id);

            if (walkModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkModel));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create(AddWalkRequestDto addWalkRequestDto)
        {
            var walkModel = mapper.Map<Walk>(addWalkRequestDto);

            walkModel = await walkRepository.CreateAsync(walkModel);
            var walkDto = mapper.Map<WalkDto>(walkModel);

            return CreatedAtAction(nameof(GetById), new { walkDto.Id }, walkDto);
        }

        [HttpPut("{id}")]
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