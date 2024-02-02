using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _config;

    public AuthController(IAuthService authService, IConfiguration config)
    {
        _authService = authService;
        _config = config;
    }

    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<LoginUserResponseDto>> Login([FromBody] LoginUserRequestDto request)
    {
        var userInfo = await _authService.Login(request.Username, request.Password);

        if (userInfo == null) return Unauthorized("Invalid username or password");

        var result = new LoginUserResponseDto
        {
            Token = GenerateJsonWebToken(userInfo),
            UserInfo = userInfo
        };

        return Ok(result);
    }

    private string GenerateJsonWebToken(UserInfo user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}