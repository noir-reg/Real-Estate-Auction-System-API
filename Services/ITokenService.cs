using System.Security.Claims;
using BusinessObjects.Dtos.Response;

namespace Services;

public interface ITokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
    public string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Task SetRefreshToken(Guid userId, string refreshToken);
}