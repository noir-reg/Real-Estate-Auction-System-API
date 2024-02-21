using System.ComponentModel.DataAnnotations;
using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Request;

public class AddAdminRequestDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [MaxLength(32, ErrorMessage = "Password cannot exceeds 32 characters")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,32}$",
        ErrorMessage =
            "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
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
    [RegularExpression("^([0-9]{3})([0-9]{1})([0-9]{2})([0-9]{6})$", ErrorMessage = "Invalid Citizen Id")]
    public string CitizenId { get; set; }

    [Required(ErrorMessage = "Phone Number is required")]
    [MaxLength(10)]
    [MinLength(10)]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"(09|03|07|08|05)([0-9]{8})\b", ErrorMessage = "Invalid Phone Number")]
    public string PhoneNumber { get; set; }
}