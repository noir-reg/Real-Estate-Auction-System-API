using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Response;

public class DeleteUserResponseDto
{
    public string PhoneNumber { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string CitizenId { get; set; }
    public string DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public Guid UserId { get; set; }
}