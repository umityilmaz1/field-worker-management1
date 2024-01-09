namespace CleanArchitecture.Domain.Entities;
public class NotificationReadRecord : BaseAuditableEntity
{
    public Guid AccountId { get; set; }
    public Guid NotificationId { get; set; }

    public virtual Notification Notification { get; set; }
    public Account Account { get; set; }
}

