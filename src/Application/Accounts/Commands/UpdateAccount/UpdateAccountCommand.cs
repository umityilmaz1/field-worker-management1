using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.Accounts.Commands.UpdateAccount;
public record UpdateAccountCommand : IRequest<ReturnData<bool>>
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

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, ReturnData<bool>>
{
    private readonly IApplicationDbContext _context;
    public UpdateAccountCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<bool>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(request.Id);
        if (account == null)
        {
            return ReturnData<bool>.Fail("Account not found.");
        }
        account.Name = request.Name;
        account.Surname = request.Surname;
        account.Mail = request.Mail;
        account.Phone = request.Phone;
        account.BirthDate = request.BirthDate;
        account.EmergencyContact = request.EmergencyContact;
        account.EmergencyContactPhone = request.EmergencyContactPhone;
        account.BloodType = request.BloodType;
        account.IsAdmin = request.IsAdmin;
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool>.Success(true);
    }
}