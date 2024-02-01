using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public UserController(IUserService userService, IConfiguration config)
    {
        _userService = userService;
        _config = config;
    }

    [HttpPost("register")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto registerUserRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);


        // var newUser = new User
        // {
        //     FirstName = registerUserRequestDto.FirstName,
        //     LastName = registerUserRequestDto.LastName,
        //     Username = registerUserRequestDto.Username,
        //     Password = registerUserRequestDto.Password,
        //     Gender = registerUserRequestDto.Gender,
        //     DateOfBirth = registerUserRequestDto.DateOfBirth,
        //     CitizenId = registerUserRequestDto.CitizenId,
        //     Email = registerUserRequestDto.Email
        // };


        try
        {
            // await _userService.AddAsync(newUser);
            return Ok("Success");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<LoginUserResponseDto>> Login([FromBody] LoginUserRequestDto loginUserRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var username = loginUserRequestDto.Username;
        var password = loginUserRequestDto.Password;

        try
        {
            // var result = await _userService.Login(username, password);
            //
            // if (result == null) return Unauthorized("Wrong username or password");
            //
            // var userInfo = new UserInfo
            // {
            //     UserId = result.UserId,
            //     Email = result.Email,
            //     Username = result.Username
            // };
            //
            // var response = new LoginUserResponseDto
            // {
            //     Token = GenerateJsonWebToken(userInfo),
            //     UserInfo = userInfo
            // };
            return Ok();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            // var result = await _userService.GetAll();
            //
            // var response = result.Select(
            //     user => new UserListResponse
            //     {
            //         UserId = user.UserId,
            //         Username = user.Username,
            //         Email = user.Email,
            //         FirstName = user.FirstName,
            //         LastName = user.LastName,
            //         Gender = user.Gender,
            //         DateOfBirth = user.DateOfBirth,
            //         CitizenId = user.CitizenId
            //     });
            //
            return Ok();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}