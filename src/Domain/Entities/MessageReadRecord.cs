namespace CleanArchitecture.Domain.Entities;
public class MessageReadRecord : BaseAuditableEntity
{
    public Guid AccountId { get; set; }
    public Guid MessageId { get; set; }

    public virtual Message Message { get; set; }
    public Account Account { get; set; }
}

