using System.Net;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Real_Estate_Auction_System_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto registerUserRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);


        var newUser = new User
        {
            FirstName = registerUserRequestDto.FirstName,
            LastName = registerUserRequestDto.LastName,
            Username = registerUserRequestDto.Username,
            Password = registerUserRequestDto.Password,
            Gender = registerUserRequestDto.Gender,
            DateOfBirth = registerUserRequestDto.DateOfBirth,
            CitizenId = registerUserRequestDto.CitizenId,
            Email = registerUserRequestDto.Email
        };


        try
        {
            await _userService.AddAsync(newUser);
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
    public async Task<IActionResult> Login([FromBody] LoginUserRequestDto loginUserRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var username = loginUserRequestDto.Username;
        var password = loginUserRequestDto.Password;

        try
        {
            var result = await _userService.Login(username, password);

            if (result == false) return BadRequest("Wrong username or password");
            return Ok("Login Success");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}