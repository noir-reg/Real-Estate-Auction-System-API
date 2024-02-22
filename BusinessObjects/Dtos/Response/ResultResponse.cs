namespace BusinessObjects.Dtos.Response;

public class ResultResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string[] Messages { get; set; } = { };
}