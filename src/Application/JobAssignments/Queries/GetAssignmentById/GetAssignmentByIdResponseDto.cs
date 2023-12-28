namespace CleanArchitecture.Application.JobAssignments.Queries.GetAssignmentById;
public class GetAssignmentByIdResponseDto
{
    public Guid AccountId { get; set; }
    public Guid JobTypeId { get; set; }
    public Guid FieldId { get; set; }
    public DateTime Date { get; set; }
    public string JobTypeName { get; set; }
    public string FieldName { get; set; }
    public string AccountName { get; set; }
}
