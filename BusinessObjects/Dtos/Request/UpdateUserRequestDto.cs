using System.ComponentModel.DataAnnotations;
using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Request;

public class UpdateUserRequestDto
{
    public string? Username { get; set; }
    [EmailAddress]
    public string? Email { get; set; }

    public Gender? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? CitizenId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
}