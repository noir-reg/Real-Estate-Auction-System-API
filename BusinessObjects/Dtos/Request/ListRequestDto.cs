namespace BusinessObjects.Dtos.Request;

public class ListRequestDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int Offset => (Page - 1) * PageSize;
}