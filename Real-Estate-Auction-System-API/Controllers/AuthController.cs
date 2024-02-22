using System.Net;
using System.Security.Claims;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _config;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, IConfiguration config, ITokenService tokenService)
    {
        _authService = authService;
        _config = config;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<LoginUserResponseDto>> Login([FromBody] LoginUserRequestDto request)
    {
        var userInfo = await _authService.Login(request.Email, request.Password);

        if (userInfo == null) return Unauthorized("Invalid email or password");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userInfo.UserId.ToString()),
            new(ClaimTypes.Name, userInfo.Email),
            new(ClaimTypes.Role, userInfo.Role)
        };

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        await _tokenService.SetRefreshToken(userInfo.UserId, refreshToken);

        var result = new LoginUserResponseDto
        {
            Token = accessToken,
            RefreshToken = refreshToken,
            UserInfo = userInfo
        };

        return Ok(result);
    }

    [HttpPost("register")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ResultResponse<RegisterMemberResponseDto>>> Register(
        [FromBody] RegisterMemberRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.Register(request);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}