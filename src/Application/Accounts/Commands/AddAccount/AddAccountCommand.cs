using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.Accounts.Commands.AddAccount;
public record AddAccountCommand : IRequest<ReturnData<bool>>
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
}

public class AddAccountCommandHandler : IRequestHandler<AddAccountCommand, ReturnData<bool>>
{
    private readonly IApplicationDbContext _context;
    public AddAccountCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<bool>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
    {
        _context.Accounts.Add(new Account
        {
            Name = request.Name,
            Surname = request.Surname,
            Mail = request.Mail,
            Phone = request.Phone,
            BirthDate = request.BirthDate,
            EmergencyContact = request.EmergencyContact,
            EmergencyContactPhone = request.EmergencyContactPhone,
            BloodType = request.BloodType,
            IsAdmin = request.IsAdmin
        });
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool>.Success(true);
    }
}