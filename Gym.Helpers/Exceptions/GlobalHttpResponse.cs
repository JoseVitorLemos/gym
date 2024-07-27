
namespace Gym.Helpers.Exceptions;

public sealed class GlobalHttpResponse
{

    public int StatusCode { get; set; }
    public string Message { get; set; }
    public object Data { get; set; } 

    public GlobalHttpResponse(int statusCode, string message, object data = null)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
}