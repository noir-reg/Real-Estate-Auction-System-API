using System.Text.Json.Serialization;

namespace BusinessObjects.Dtos.Request;

public class BaseQueryDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    
    [JsonIgnore]
    public int Offset => (Page - 1) * PageSize;
}

public enum OrderDirection
{
    ASC,
    DESC
}

public class UserQuery : BaseQueryDto
{
    public UserSortBy SortBy { get; set; }
    public OrderDirection OrderDirection { get; set; }
    public SearchUserQuery? Search { get; set; }
}

public enum UserSortBy
{
    Username
}

public enum UserRole
{
    Admin,
    Member,
    Staff
}

public class SearchUserQuery
{
    public string? Username { get; set; }
}