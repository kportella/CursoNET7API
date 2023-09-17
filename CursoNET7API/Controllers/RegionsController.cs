using CursoNET7API.Data;
using CursoNET7API.Models.Domain;
using CursoNET7API.Models.DTO;
using CursoNET7API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoNET7API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository) 
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();

            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            var region = await dbContext.Regions.FindAsync(id);

            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null) { return NotFound(); }

            var regionDto = new RegionDto() { Id = region.Id, Code = region.Code, Name = region.Name, RegionImageUrl = region.RegionImageUrl };

            return Ok(regionDto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionDto) 
        {
            var regionModel = new Region
            {
                Code = regionDto.Code,
                Name = regionDto.Name,
                RegionImageUrl = regionDto.RegionImageUrl,
            };

            await dbContext.Regions.AddAsync(regionModel);
            await dbContext.SaveChangesAsync();

            var regionDtoReturn = new RegionDto
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                RegionImageUrl = regionModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDtoReturn.Id}, regionDtoReturn);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionModel = await dbContext.Regions.FindAsync(id);

            if (regionModel == null) { return NotFound(); }

            regionModel.Code = updateRegionRequestDto.Code;
            regionModel.Name = updateRegionRequestDto.Name;
            regionModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            var regionDto = new RegionDto
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                RegionImageUrl = regionModel.RegionImageUrl,
            };

            return Ok(regionDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionModel = await dbContext.Regions.FindAsync(id);

            if (regionModel == null) { return NotFound(); }

            dbContext.Regions.Remove(regionModel);
            await dbContext.SaveChangesAsync();

            var regionDto = new RegionDto
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                RegionImageUrl = regionModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

    }
}
