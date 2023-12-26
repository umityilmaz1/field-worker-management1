using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.Accounts.Queries.GetAccountById;
public record GetAccountByIdQuery : IRequest<ReturnData<GetAccountByIdQueryResponseDto>>
{
    public Guid Id { get; set; }
}

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, ReturnData<GetAccountByIdQueryResponseDto>>
{
    private readonly IApplicationDbContext _context;
    public GetAccountByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<GetAccountByIdQueryResponseDto>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(request.Id);

        if (account == null)
        {
            return ReturnData<GetAccountByIdQueryResponseDto>.Fail("Account not found.");
        }

        return ReturnData<GetAccountByIdQueryResponseDto>.Success(new GetAccountByIdQueryResponseDto
        {
            Id = account.Id,
            Name = account.Name,
            Surname = account.Surname,
            Mail = account.Mail,
            Phone = account.Phone,
            BirthDate = account.BirthDate,
            EmergencyContact = account.EmergencyContact,
            EmergencyContactPhone = account.EmergencyContactPhone,
            BloodType = account.BloodType,
            IsAdmin = account.IsAdmin
        });
    }
}