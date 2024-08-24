using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        { 
            //Get Data from Database - Domain models
            var regions = _dbContext.Regions.ToList();

            //Map Domain models to DTO's
            var regionDto = new List<RegionDto>();
            foreach(var region in regions)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            return Ok(regions);
        } 

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(region == null)
            return NotFound();

            return Ok(region);
        }
    }
}
