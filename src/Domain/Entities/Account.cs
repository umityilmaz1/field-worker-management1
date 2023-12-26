namespace CleanArchitecture.Domain.Entities;
public class Account : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Mail { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public string EmergencyContact { get; set; }
    public string EmergencyContactPhone { get; set; }
    public string BloodType { get; set; }
    public bool IsAdmin { get; set; }

    public virtual ICollection<JobType> JobTypes { get; set; }
}
