﻿using AutoMapper;
using CursoNET7API.CustomActionFilters;
using CursoNET7API.Data;
using CursoNET7API.Models.Domain;
using CursoNET7API.Models.DTO;
using CursoNET7API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoNET7API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper) 
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();

            return Ok(mapper.Map<List<RegionDto>>(regions));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null) { return NotFound(); }

            return Ok(mapper.Map<RegionDto>(region));
        }


        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            var regionModel = await regionRepository.CreateAsync(mapper.Map<Region>(addRegionRequestDto));

            var regionDto = mapper.Map<RegionDto>(regionModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionModel = await regionRepository.UpdateAsync(id, mapper.Map<Region>(updateRegionRequestDto));

            if (regionModel == null) { return NotFound(); }

            return Ok(mapper.Map<RegionDto>(regionModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionModel = await regionRepository.DeleteAsync(id);

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

            return Ok(mapper.Map<RegionDto>(regionModel));
        }

    }
}
