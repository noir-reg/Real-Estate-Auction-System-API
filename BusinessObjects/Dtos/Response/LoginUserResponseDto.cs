using BusinessObjects.Enums;

namespace BusinessObjects.Dtos.Response;

public class UserInfo
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string PhoneNumber { get; set; }
}

public class LoginUserResponseDto
{
    public string Token { get; set; }
    public UserInfo UserInfo { get; set; }
    public string RefreshToken { get; set; }
}