using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.JobAssignments.Queries.GetAssignmentById;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.JobAssignments.Queries.GetAssignmentsByAccountId;
public record GetAssignmentsByAccountIdQuery : IRequest<ReturnData<List<GetAssignmentsByAccountIdResponseDto>>>
{
    public Guid AccountId { get; set; }
}

public class GetAssignmentsByAccountIdQueryHandler : IRequestHandler<GetAssignmentsByAccountIdQuery, ReturnData<List<GetAssignmentsByAccountIdResponseDto>>>
{
    private readonly IApplicationDbContext _context;
    public GetAssignmentsByAccountIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<List<GetAssignmentsByAccountIdResponseDto>>> Handle(GetAssignmentsByAccountIdQuery request, CancellationToken cancellationToken)
    {
        var jobAssignments = await _context.JobAssignments
            .Where(x => x.AccountId == request.AccountId)
            .Include(x => x.JobType)
            .Include(x => x.Field)
            .ToListAsync(cancellationToken);

        var response = jobAssignments.Select(x => new GetAssignmentsByAccountIdResponseDto
        {
            AssignmentId = x.Id,
            Date = x.Date,
            JobTypeName = x.JobType.Name,
            FieldName = x.Field.Name
        }).ToList();

        return ReturnData<List<GetAssignmentsByAccountIdResponseDto>>.Success(response);
    }
}