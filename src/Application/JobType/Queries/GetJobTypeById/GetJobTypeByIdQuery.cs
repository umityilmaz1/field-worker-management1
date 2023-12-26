using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Accounts.Queries.GetAccountById;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.JobType.Queries.GetJobTypeById;
public class GetJobTypeByIdQuery : IRequest<ReturnData<GetJobTypeByIdQueryResponseDTO>>
{
    public Guid Id { get; set; }
}

public class GetJobTypeByIdQueryHandler : IRequestHandler<GetJobTypeByIdQuery, ReturnData<GetJobTypeByIdQueryResponseDTO>>
{
    private readonly IApplicationDbContext _context;
    public GetJobTypeByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<GetJobTypeByIdQueryResponseDTO>> Handle(GetJobTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var jobType = await _context.JobTypes.FindAsync(request.Id);

        if (jobType == null)
        {
            return ReturnData<GetJobTypeByIdQueryResponseDTO>.Fail("Account not found.");
        }

        return ReturnData<GetJobTypeByIdQueryResponseDTO>.Success(new GetJobTypeByIdQueryResponseDTO
        {
            Id = jobType.Id,
            Name = jobType.Name,
            Accounts = jobType.Accounts
        });
    }
}