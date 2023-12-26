using System.Net;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.Common.Exceptions;
public class ConflictException : BaseException
{
    public ConflictException(string message, ErrorMessageReceiver receiver) : base(message, HttpStatusCode.Conflict, receiver)
    {
    }
}
