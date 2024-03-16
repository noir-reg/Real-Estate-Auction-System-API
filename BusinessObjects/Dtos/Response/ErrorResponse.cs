namespace BusinessObjects.Dtos.Response;

public static class ErrorResponse
{
    
   public static ResultResponse<T> CreateErrorResponse<T>(Exception? e = null,string? message = null)
    {
        return new ResultResponse<T>()
        {
            IsSuccess = false,
            Messages = new[] { e?.Message,e?.InnerException?.Message,e?.StackTrace,message },
            Status = Status.Error
        };
    }}