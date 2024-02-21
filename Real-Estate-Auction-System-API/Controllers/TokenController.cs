using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/token")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public TokenController(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<LoginUserResponseDto>> Refresh([FromBody] RefreshTokenRequestDto request)
    {
        if (request == null) return Unauthorized("Invalid client request");

        var accessToken = request.AccessToken;
        var refreshToken = request.RefreshToken;
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        var userName = principal.Identity.Name;

        var user = await _userRepository.GetUserAsync(e => e.Username == userName);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return Unauthorized("Invalid client request");

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userRepository.Update(user);

        var response = new LoginUserResponseDto
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken,
            UserInfo = new UserInfo
            {
                Email = user.Email,
                Username = user.Username,
                Role = user.Role, UserId = user.UserId
            }
        };

        return Ok(response);
    }

    [HttpPost("revoke")]
    [Authorize]
    public async Task<IActionResult> Revoke()
    {
        var username = User.Identity.Name;

        var user = await _userRepository.GetUserAsync(e => e.Username == username);
        if (user is null) return BadRequest();

        user.RefreshToken = null;
        await _userRepository.Update(user);

        return NoContent();
    }
}