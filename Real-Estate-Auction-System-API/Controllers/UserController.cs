using System.Security.Claims;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult<ListResponseBaseDto<UserListResponseDto>>> GetList([FromQuery] UserQuery request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var data = await _userService.GetUsersAsync(request);
        return Ok(data);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult<UserDetailResponseDto>> GetDetail([FromRoute] Guid id)
    {
        var data = await _userService.GetUserAsync(x => x.UserId == id);
        return Ok(data);
    }
    
    [HttpPost()]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ResultResponse<CreateUserResponseDto>>> CreateUser([FromBody] CreateUserRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userService.CreateUserAsync(request);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ResultResponse<DeleteUserResponseDto>>> DeleteUser([FromRoute] Guid id)
    {
        var currentId = Guid.Parse(User.FindFirst(claim => ClaimTypes.NameIdentifier.Equals(claim.Type))?.Value ?? string.Empty);
        if (id == currentId)
        {
            return Unauthorized();
        }
        
        var data = await _userService.DeleteUserAsync(id);
        if (data?.Status == Status.NotFound)
        {
            return NotFound(data);
        }
        return Ok(data);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ResultResponse<UpdateUserResponseDto>>> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequestDto request)
    {
        ResultResponse<UpdateUserResponseDto> data = await _userService.UpdateUserAsync(id, request);
        if (data.Status == Status.NotFound)
        {
            return NotFound(data);
        }
        return Ok(data);
    }
}