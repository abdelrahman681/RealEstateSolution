using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Error;
using RealEstate.Domain.DTO;
using RealEstate.Domain.InterFace.Services;
using RealEstate.Domain.InterFace.Specification;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        private readonly IPropertyServices _services;

        public HomesController(IPropertyServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyToReturnDto>>> GetAllProperties([FromQuery] PropertySpecificationParameters parameters)
        {
            return Ok(await _services.GetAllPropertyAysnc(parameters));
        }

        [HttpGet("Catagory")]
        public async Task<ActionResult<IEnumerable<CategoryToRetuenDto>>> GetAllCategories()
        {
            return Ok(await _services.GetAllCategoryAysnc());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PropertyToReturnDto>> GetpropertyById(int Id)
        {
            if (Id == null) return BadRequest();
            var property = await _services.GetByIdPropertyAysnc(Id);
            return property is not null ? Ok(property) : NotFound(new APIResponse(404, $"The Property with id value {Id} Not Found"));
        }
    }
}
