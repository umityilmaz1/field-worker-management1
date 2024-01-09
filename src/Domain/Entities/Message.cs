namespace CleanArchitecture.Domain.Entities;
public class Message : BaseAuditableEntity
{
    public Guid ReceiverId { get; set; }
    public string Content { get; set; }

    public virtual ICollection<MessageReadRecord> MessageReadRecords { get; set; }
    public virtual Account Receiver { get; set; }
}

