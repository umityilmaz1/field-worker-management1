namespace CleanArchitecture.Application.JobAssignments.Queries.GetAssignmentById;
public class GetAssignmentsByAccountIdResponseDto
{
    public Guid AssignmentId { get; set; }
    public DateTime Date { get; set; }
    public string JobTypeName { get; set; }
    public string FieldName { get; set; }
}
