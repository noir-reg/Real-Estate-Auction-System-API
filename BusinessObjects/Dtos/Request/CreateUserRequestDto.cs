using System.ComponentModel.DataAnnotations;
using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Request;

public class CreateUserRequestDto
{
    public string Username { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    [Compare("Password",ErrorMessage = "Confirm password doesn't match, Type again !")]
    public string ConfirmPassword { get; set; }
    [Required]
    
    public string Role { get; set; }
    public Gender Gender { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    public string CitizenId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}