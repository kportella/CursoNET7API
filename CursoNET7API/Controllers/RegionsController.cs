using CursoNET7API.Data;
using CursoNET7API.Models.Domain;
using CursoNET7API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoNET7API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();

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
        public IActionResult GetById([FromRoute] Guid id) 
        {
            var region = dbContext.Regions.Find(id);

            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null) { return NotFound(); }

            var regionDto = new RegionDto() { Id = region.Id, Code = region.Code, Name = region.Name, RegionImageUrl = region.RegionImageUrl };

            return Ok(regionDto);
        }


        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto regionDto) 
        {
            var regionModel = new Region
            {
                Code = regionDto.Code,
                Name = regionDto.Name,
                RegionImageUrl = regionDto.RegionImageUrl,
            };

            dbContext.Regions.Add(regionModel);
            dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionModel = dbContext.Regions.Find(id);

            if (regionModel == null) { return NotFound(); }

            regionModel.Code = updateRegionRequestDto.Code;
            regionModel.Name = updateRegionRequestDto.Name;
            regionModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

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
