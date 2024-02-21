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
    public async Task<IActionResult> AddAdminAsync([FromBody] AddAdminRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _adminService.AddAdminAsync(request);
        return Ok("Create successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAdminAsync([FromRoute] Guid id, [FromBody] UpdateAdminRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _adminService.UpdateAdminAsync(id, request);
        return Ok("Update successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdminAsync([FromRoute] Guid id)
    {
        await _adminService.DeleteAdminAsync(id);
        return Ok("Delete Successfully");
    }
}