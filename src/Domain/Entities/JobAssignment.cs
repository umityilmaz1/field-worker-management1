namespace CleanArchitecture.Domain.Entities;
public class JobAssignment : BaseAuditableEntity
{
    public Guid AccountId { get; set; }
    public Guid JobTypeId { get; set; }
    public Guid FieldId { get; set; }
    public DateTime Date { get; set; }

    public virtual JobType JobType { get; set; }
    public virtual Field Field { get; set; }
    public virtual Account Account { get; set; }
}
