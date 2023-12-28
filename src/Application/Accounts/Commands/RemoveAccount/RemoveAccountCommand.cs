using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.Accounts.Commands.RemoveAccount;
public record RemoveAccountCommand : IRequest<ReturnData<bool>>
{
    public Guid Id { get; set; }
}

public class RemoveAccountCommandHandler : IRequestHandler<RemoveAccountCommand, ReturnData<bool>>
{
    private readonly IApplicationDbContext _context;
    public RemoveAccountCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<bool>> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(request.Id);
        if (account == null)
        {
            return ReturnData<bool>.Fail("Account not found.");
        }
        account.IsDeleted = true;
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool>.Success(true);
    }
}
