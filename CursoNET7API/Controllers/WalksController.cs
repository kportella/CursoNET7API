using AutoMapper;
using CursoNET7API.CustomActionFilters;
using CursoNET7API.Models.Domain;
using CursoNET7API.Models.DTO;
using CursoNET7API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CursoNET7API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
           var walkModel = await walkRepository.CreateAsync(mapper.Map<Walk>(addWalkRequestDto));

           return Ok(mapper.Map<WalkDto>(walkModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {

            return Ok(mapper.Map<List<WalkDto>>(await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true)));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var walkModel = await walkRepository.GetByIdAsync(id);
            if (walkModel == null) return NotFound();
            return Ok(mapper.Map<WalkDto>(walkModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkDto)
        {
            var walkModel = await walkRepository.UpdateAsync(id, mapper.Map<Walk>(updateWalkDto));
            if (walkModel == null) return NotFound();
            return Ok(mapper.Map<WalkDto>(walkModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkModel = await walkRepository.DeleteAsync(id);

            if (walkModel == null) return NotFound();
            return Ok(mapper.Map<WalkDto>(walkModel));
        }
    }
}
