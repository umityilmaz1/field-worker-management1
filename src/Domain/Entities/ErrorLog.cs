namespace CleanArchitecture.Domain.Entities;
public class ErrorLog : BaseAuditableEntity
{
    public int?  UserId { get; set; }
    public string ExceptionType { get; set; }
    public string Message { get; set; }
    public string? StackTrace { get; set; }
    public string? InnerMessage { get; set; }
    public string? InnerStackTrace { get; set; }
}
