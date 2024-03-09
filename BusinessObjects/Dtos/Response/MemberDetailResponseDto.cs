using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Response;

public class MemberDetailResponseDto
{
    public Guid MemberId { get; set; }
    public string CitizenId { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public Gender Gender { get; set; }
    public string Role { get; set; }
}