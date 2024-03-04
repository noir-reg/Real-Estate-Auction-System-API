using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Response;

public class AddStaffResponseDto
{
    public Guid StaffId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public string CitizenId { get; set; }
}