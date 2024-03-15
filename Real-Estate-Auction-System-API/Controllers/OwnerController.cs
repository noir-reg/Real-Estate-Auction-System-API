using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Net;

namespace Real_Estate_Auction_System_API.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService= ownerService;
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<ListResponseBaseDto<OwnerResponse>>> GetPaginationList([FromQuery] OwnerQuery request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var data = await _ownerService.GetOwnersAsync(request);
            return Ok(data);
        }
        [HttpGet]
         
        public ActionResult<List<RealEstateOwner>> GetList()
        {
            var result = _ownerService.GetAllOwners();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultResponse<OwnerResponse>>> GetDetail([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var data = await _ownerService.GetOwnerAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<ResultResponse<AddOwnerResponseDto>>> Create([FromBody] AddOwnerRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _ownerService.AddOwnerAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResultResponse<OwnerUpdateResponseDto>>> Update([FromBody] OwnerUpdateRequestDto request, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _ownerService.UpdateOwnerAsync(id, request);

            if (result.Status == Status.NotFound)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultResponse<DeleteStaffResponseDto>>> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _ownerService.DeleteOwnerAsync(id);
            if (result.Status == Status.Error) return BadRequest();
            if (result.Status == Status.NotFound) return NotFound(result);
            return Ok(result);
        }
    }
}
