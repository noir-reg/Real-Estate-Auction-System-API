using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/staffs")]
public class StaffController : ControllerBase
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    [HttpGet]
    public async Task<ActionResult<ListResponseBaseDto<StaffListResponseDto>>> GetList([FromQuery] StaffQuery request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var data = await _staffService.GetStaffsAsync(request);
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResultResponse<StaffDetailResponseDto>>> GetDetail([FromRoute] Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var data = await _staffService.GetStaffAsync(id);
        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult<ResultResponse<AddStaffResponseDto>>> Create([FromBody] AddStaffRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

       var result = await _staffService.AddStaffAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResultResponse<UpdateStaffResponseDto>>> Update([FromBody] UpdateStaffRequestDto request, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
       var result = await _staffService.UpdateStaffAsync(id, request);

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

        var result = await _staffService.DeleteStaffAsync(id);
        if (result.Status != Status.NotFound) return NotFound(result);
        return Ok(result);
    }
}