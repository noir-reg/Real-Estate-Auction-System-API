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
    public async Task<IActionResult> Create([FromBody] AddStaffRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _staffService.AddStaffAsync(request);
        return Ok("Create successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateStaffRequestDto request, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _staffService.UpdateStaffAsync(id, request);
        return Ok("Update successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _staffService.DeleteStaffAsync(id);
        return Ok("Delete successfully");
    }
}