namespace Clean.Arch.Helpers.Exceptions;

public sealed class GlobalHttpResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public object? Data { get; set; }

    public GlobalHttpResponse(int statusCodes, string message, object? data = null)
    {
        StatusCode = statusCodes;
        Message = message;
        Data = data;
    }
}
