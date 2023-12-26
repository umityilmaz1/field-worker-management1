namespace CleanArchitecture.Application.Accounts.Queries.GetAccountById;
public class GetAccountByIdQueryResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Mail { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public string EmergencyContact { get; set; }
    public string EmergencyContactPhone { get; set; }
    public string BloodType { get; set; }
    public bool IsAdmin { get; set; }
}
