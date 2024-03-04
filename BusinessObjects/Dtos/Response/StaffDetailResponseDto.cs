using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Response;

public class StaffDetailResponseDto : UserDetailResponseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string CitizenId { get; set; }
}