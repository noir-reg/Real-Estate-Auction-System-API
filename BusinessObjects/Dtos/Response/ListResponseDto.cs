namespace BusinessObjects.Dtos.Response;

public class ListResponseDto<T>
{
    public List<T> Items { get; set; } = new();
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public bool HasNext => Page < TotalPages;

    public bool HasPrevious => Page > 1;

    public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
}