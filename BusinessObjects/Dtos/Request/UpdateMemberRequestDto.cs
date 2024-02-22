using System.ComponentModel.DataAnnotations;
using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Request;

public class UpdateMemberRequestDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }

    [MaxLength(10)]
    [MinLength(10)]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"(09|03|07|08|05)([0-9]{8})\b", ErrorMessage = "Invalid Phone Number")]
    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
}