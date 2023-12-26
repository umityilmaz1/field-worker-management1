using System.Net;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Domain.Common;
public abstract class BaseException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public ErrorMessageReceiver Receiver { get; set; }
    public BaseException(string message, HttpStatusCode statusCode, ErrorMessageReceiver receiver) : base(message)
    {
        StatusCode = statusCode;
        Receiver = receiver;
    }
    public BaseException(HttpStatusCode statusCode, ErrorMessageReceiver receiver)
    {
        StatusCode = statusCode;
        Receiver = receiver;
    }
}
