﻿using AutoMapper;
using CursoNET7API.Models.Domain;
using CursoNET7API.Models.DTO;
using CursoNET7API.Repositories;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkModel = await walkRepository.CreateAsync(mapper.Map<Walk>(addWalkRequestDto));

            return Ok(mapper.Map<WalkDto>(walkModel));
 
        }
    }
}
