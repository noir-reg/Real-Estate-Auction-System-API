namespace BusinessObjects.Dtos.Response;

public class ResultResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string?[] Messages { get; set; } = { };
    public Status Status { get; set; }
}

public enum Status
{
    Ok,
    NotFound,
    Duplicate,
    Error
}