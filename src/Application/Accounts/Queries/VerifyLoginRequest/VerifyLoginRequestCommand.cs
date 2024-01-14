using AutoMapper;
using CleanArchitecture.Application.Accounts.Queries.GetAccounts;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Accounts.Queries.VerifyLoginRequest;
public record VerifyLoginRequestCommand : IRequest<ReturnData<Account?>>
{
    public string Phone { get; set; }
    public string Password { get; set; }
}

public class VerifyLoginRequestCommandHandler : IRequestHandler<VerifyLoginRequestCommand, ReturnData<Account?>>
{
    private readonly IApplicationDbContext _context;
    public VerifyLoginRequestCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<Account?>> Handle(VerifyLoginRequestCommand request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Phone == request.Phone && a.Password == request.Password & !a.IsActive && !a.IsDeleted);

        if (account == null)
        {
            return ReturnData<Account?>.Fail("Phone or password is wrong.");
        }

        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<Account?>.Success(account);
    }
}