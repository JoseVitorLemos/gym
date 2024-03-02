using Clean.Arch.Helpers.Enums;
using Clean.Arch.Helpers.Validations;

namespace Clean.Arch.Helpers.Exceptions;

public sealed class GlobalException : Exception
{
    public HttpStatusCodes StatusCode { get; set; }

    public GlobalException(HttpStatusCodes statusCode, string message) : base(message)
        => StatusCode = statusCode;

    public static void When(bool hasError, string error, HttpStatusCodes statusCode = (HttpStatusCodes)400)
    {
        if (!EnumValidations.IsValidEnum<HttpStatusCodes>(statusCode))
            throw new GlobalException((HttpStatusCodes)statusCode,
                    "Invalid enum statusCode provided");

        if (hasError)
            throw new GlobalException((HttpStatusCodes)statusCode, error);
    }
}
