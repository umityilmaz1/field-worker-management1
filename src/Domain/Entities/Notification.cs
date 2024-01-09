namespace CleanArchitecture.Domain.Entities;
public class Notification : BaseAuditableEntity
{
    public string Content { get; set; }

    public virtual ICollection<NotificationReadRecord> NotificationReadRecords { get; set; }
}

