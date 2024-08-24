using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._regionRepository = regionRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            //Get Data from Database - Domain models
            var regions = await _regionRepository.GetAllAsync();
#region 
            //Map Domain models to DTO's
            // var regionDto = new List<RegionDto>();
            // foreach(var region in regions)
            // {
            //     regionDto.Add(new RegionDto()
            //     {
            //         Id = region.Id,
            //         Name = region.Name,
            //         Code = region.Code,
            //         RegionImageUrl = region.RegionImageUrl
            //     });
            // }
#endregion
            var regionDto = _mapper.Map<List<RegionDto>>(regions);

            //Return DTOs
            return Ok(regionDto);
        } 

        [HttpGet]
        [Route("{id:guid}")]
        public async Task <IActionResult> GetById([FromRoute] Guid id) 
        {
            var region = await _regionRepository.GetByIdAsync(id);

            if(region == null)
            return NotFound();
#region 
            //Map Region Domain model to DTO
            //  var regionDto = new RegionDto
            //  {
            //     Id = region.Id,
            //     Name = region.Name,
            //     Code = region.Code,
            //     RegionImageUrl = region.RegionImageUrl
            //  };
#endregion

            return Ok(_mapper.Map<RegionDto>(region));
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            #region 
            //Map DTO to domain model
            // var regionDomainModel = new Region
            // {
            //     Name = addRegionRequestDto.Name,
            //     Code = addRegionRequestDto.Code,
            //     RegionImageUrl = addRegionRequestDto.RegionImageUrl
            // };
            #endregion
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            //Map Domain Model to DTO
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            #region 
            // var regionDto = new RegionDto
            // {
            //     Id = regionDomainModel.Id,
            //     Name = regionDomainModel.Name,
            //     Code = regionDomainModel.Code,
            //     RegionImageUrl = regionDomainModel.RegionImageUrl
            // };
            #endregion
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task <IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map Dto to domain Model
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

            #region 
            // var regionDomainModel = new Region
            // {
            //     Code = updateRegionRequestDto.Code,
            //     Name = updateRegionRequestDto.Name,
            //     RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            // };
            #endregion

            //Check if region exists
            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if(regionDomainModel == null)
            return NotFound();
#region 
            //Map Domain model to DTO

            // var regionDto = new Region
            // {
            //     Id = regionDomainModel.Id,
            //     Name = regionDomainModel.Name,
            //     Code = regionDomainModel.Code,
            //     RegionImageUrl = regionDomainModel.RegionImageUrl
            // };
#endregion
            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var regionDomainModel = await _regionRepository.DeleteAsync(id);

            if(regionDomainModel == null)
            return NotFound();
#region 
            //Return Deleted Region back -- Map Domain model to Dto
            // var regionDto = new RegionDto
            // {
            //     Id = regionDomainModel.Id,
            //     Name = regionDomainModel.Name, 
            //     Code = regionDomainModel.Code,
            //     RegionImageUrl = regionDomainModel.RegionImageUrl
            // };
#endregion
            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
