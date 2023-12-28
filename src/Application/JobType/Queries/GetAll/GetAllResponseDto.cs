namespace CleanArchitecture.Application.JobType.Queries.GetAll;
public class GetAllResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
}
