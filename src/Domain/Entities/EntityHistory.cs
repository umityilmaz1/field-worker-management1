using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities;
public class EntityHistory : BaseAuditableEntity
{
    public EntityHistory()
    {

    }

    public EntityHistory(Guid entityId, string entityType, string propertyName, string propertyType, string newValue, string oldValue, EntityHistoryChangeType changeType, Guid transactionId, DateTime createdDate, string? createdBy)
    {
        EntityId = entityId;
        EntityType = entityType;
        PropertyName = propertyName;
        PropertyType = propertyType;
        NewValue = newValue;
        OldValue = oldValue;
        ChangeType = changeType;
        TransactionId = transactionId;
        CreatedDate = createdDate;
        CreatedBy = createdBy;
    }
    public Guid EntityId { get; set; }
    public string EntityType { get; set; }
    public string PropertyName { get; set; }
    public string PropertyType { get; set; }
    public string? NewValue { get; set; }
    public string? OldValue { get; set; }
    public EntityHistoryChangeType ChangeType { get; set; }
    public Guid TransactionId { get; set; }

}