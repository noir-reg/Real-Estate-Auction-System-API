using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/admins")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<ActionResult<ListResponseBaseDto<AdminListResponseDto>>> GetAdminsAsync(
        [FromQuery] AdminQuery request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var data = await _adminService.GetAdminsAsync(request);

        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult<ResultResponse<AddAdminResponseDto>>> AddAdminAsync(
        [FromBody] AddAdminRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _adminService.AddAdminAsync(request);
        if (result.Status == Status.Duplicate)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateAdminResponseDto>> UpdateAdminAsync([FromRoute] Guid id,
        [FromBody] UpdateAdminRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _adminService.UpdateAdminAsync(id, request);
        if (result.Status == Status.NotFound) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResultResponse<DeleteAdminResponseDto>>> DeleteAdminAsync([FromRoute] Guid id)
    {
        var result = await _adminService.DeleteAdminAsync(id);
        if (result.Status == Status.NotFound) return NotFound(result);
        return Ok(result);
    }
}