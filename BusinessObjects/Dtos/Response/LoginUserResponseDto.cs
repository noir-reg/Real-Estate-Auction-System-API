namespace BusinessObjects.Dtos.Response;

public class UserInfo
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}

public class LoginUserResponseDto
{
    public string Token { get; set; }
    public UserInfo UserInfo { get; set; }
}