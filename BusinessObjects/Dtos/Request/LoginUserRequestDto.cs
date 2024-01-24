using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Dtos.Request;

public class LoginUserRequestDto
{
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }
}