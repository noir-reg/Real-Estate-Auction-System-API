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

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ListResponseBaseDto<UserListResponseDto>>> GetList([FromQuery] UserQuery request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var data = await _userService.GetUsersAsync(request);
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailResponseDto>> GetDetail([FromRoute] Guid id)
    {
        var data = await _userService.GetUserAsync(x => x.UserId == id);
        return Ok(data);
    }
}