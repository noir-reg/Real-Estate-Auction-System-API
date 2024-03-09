using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Response;

public class UpdateUserResponseDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public Gender Gender { get; set; }
    public string DateOfBirth { get; set; }
    public string CitizenId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}