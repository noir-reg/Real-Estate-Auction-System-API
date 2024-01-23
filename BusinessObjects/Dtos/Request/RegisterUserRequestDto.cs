using System.ComponentModel.DataAnnotations;
using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Request;

public class RegisterUserRequestDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    [EnumDataType(typeof(Gender))]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Citizen Id is required")]
    [MaxLength(12)]
    [MinLength(12)]
    public string CitizenId { get; set; }
}