using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepostiroy _walkRepostiroy;

        public WalksController(IMapper mapper, IWalkRepostiroy walkRepostiroy)
        {
            this._mapper = mapper;
            this._walkRepostiroy = walkRepostiroy;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map DTO to Domain model
            var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);

            await _walkRepostiroy.CreateAsync(walkDomainModel);

            //Map Domain model to DTO
            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}
