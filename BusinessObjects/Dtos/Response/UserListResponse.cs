using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Response;

public class UserListResponse
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CitizenId { get; set; }
    public string Role { get; set; }
}