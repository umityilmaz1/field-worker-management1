namespace CleanArchitecture.Application.Common.Exceptions;

public class HandledException : Exception
{
    public HandledException(string message)
        : base(message)
    {
    }
}
