using System.Net;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAdminService _adminService;
    private readonly IStaffService _staffService;
    private readonly IMemberService _memberService;

    public UserController(IUserService userService, IAdminService adminService, IStaffService staffService, IMemberService memberService)
    {
        _userService = userService;
        _adminService = adminService;
        _staffService = staffService;
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<ActionResult<ListResponseBaseDto<UserListResponseDto>>> GetList([FromQuery] UserQuery request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var data = await _userService.GetUsersAsync(request);
        return Ok(data);
    }
    
    [HttpPost("/change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        await _userService.ChangePasswordAsync(request);
        return Ok("Change password successfully");
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailResponseDto>> GetUserDetail([FromRoute] Guid id)
    {
        var data = await _userService.GetUserAsync(x => x.UserId == id);
        return Ok(data);
    }
    [HttpGet("admins")]
    public async Task<ActionResult<ListResponseBaseDto<AdminListResponseDto>>> GetAdminsAsync(
        [FromQuery] AdminQuery request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var data = await _adminService.GetAdminsAsync(request);

        return Ok(data);
    }

    [HttpPost("admins")]
    public async Task<IActionResult> AddAdminAsync([FromBody] AddAdminRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _adminService.AddAdminAsync(request);
        return Ok("Create successfully");
    }

    [HttpPut("admins/{id}")]
    public async Task<IActionResult> UpdateAdminAsync([FromRoute] Guid id, [FromBody] UpdateAdminRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _adminService.UpdateAdminAsync(id, request);
        return Ok("Update successfully");
    }

    [HttpDelete("admins/{id}")]
    public async Task<IActionResult> DeleteAdminAsync([FromRoute] Guid id)
    {
        await _adminService.DeleteAdminAsync(id);
        return Ok("Delete Successfully");
    } 
    
      [HttpGet("staffs")]
        public async Task<ActionResult<ListResponseBaseDto<StaffListResponseDto>>> GetList([FromQuery] StaffQuery request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
    
            var data = await _staffService.GetStaffsAsync(request);
            return Ok(data);
        }
    
        [HttpGet("staffs/{id}")]
        public async Task<ActionResult<ResultResponse<StaffDetailResponseDto>>> GetDetail([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
    
            var data = await _staffService.GetStaffAsync(id);
            return Ok(data);
        }
    
        [HttpPost("staffs")]
        public async Task<IActionResult> Create([FromBody] AddStaffRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
    
            await _staffService.AddStaffAsync(request);
            return Ok("Create successfully");
        }
    
        [HttpPut("staffs/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateStaffRequestDto request, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
    
            await _staffService.UpdateStaffAsync(id, request);
            return Ok("Update successfully");
        }
    
        [HttpDelete("staffs/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
    
            await _staffService.DeleteStaffAsync(id);
            return Ok("Delete successfully");
        }
        
        [HttpGet("members")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<ListResponseBaseDto<MemberListResponseDto>>> GetAll([FromQuery] MemberQuery request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _memberService.GetMembersAsync(request);
            return Ok(result);
        }

        [HttpPut("members/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<ResultResponse<UpdateMemberResponseDto>>> UpdateMember(
            [FromBody] UpdateMemberRequestDto request, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _memberService.UpdateMemberAsync(id, request);

            if (result.Status == Status.NotFound) return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("members/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<ResultResponse<DeleteMemberResponseDto>>> DeleteMember([FromRoute] Guid id)
        {
            var result = await _memberService.DeleteMemberAsync(id);


            if (result.Status == Status.NotFound) return NotFound(result);

            return Ok(result);
        }

        [HttpGet("members/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ResultResponse<MemberDetailResponseDto>>> GetMember([FromRoute] Guid id)
        {
            var result = await _memberService.GetMemberAsync(id);

            if (result.Status == Status.NotFound) return NotFound(result);

            return Ok(result);
        }
}