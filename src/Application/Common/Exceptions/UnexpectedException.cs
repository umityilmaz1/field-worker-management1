namespace CleanArchitecture.Application.Common.Exceptions;

public class UnexpectedException : Exception
{
    public UnexpectedException(string message)
        : base(message)
    {
    }
}
