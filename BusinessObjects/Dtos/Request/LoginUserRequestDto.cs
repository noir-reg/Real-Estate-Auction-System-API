using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Dtos.Request;

public class LoginUserRequestDto
{
    [Required] [EmailAddress] public string Email { get; set; }


    [Required] public string Password { get; set; }
}