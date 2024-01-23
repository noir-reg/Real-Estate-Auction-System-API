using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Dtos.Response;

public class LoginUserRequestDto
{
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }
}