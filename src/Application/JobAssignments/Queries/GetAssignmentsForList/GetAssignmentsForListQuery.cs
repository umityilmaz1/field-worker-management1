using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.JobAssignments.Queries.GetAssignmentById;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.JobAssignments.Queries.GetAssignmentsForList;
public record GetAssignmentsForListQuery : IRequest<ReturnData<List<GetAssignmentsForListResponseDto>>>
{
}

public class GetAssignmentsForListQueryHandler : IRequestHandler<GetAssignmentsForListQuery, ReturnData<List<GetAssignmentsForListResponseDto>>>
{
    private readonly IApplicationDbContext _context;
    public GetAssignmentsForListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<List<GetAssignmentsForListResponseDto>>> Handle(GetAssignmentsForListQuery request, CancellationToken cancellationToken)
    {
        var assignments = await _context.JobAssignments
            .Include(a => a.JobType)
            .Include(a => a.Field)
            .Include(a => a.Account)
            .Where(a => !a.IsDeleted)
            .Select(a => new GetAssignmentsForListResponseDto
            {
                AssignmentId = a.Id,
                Date = a.Date,
                JobTypeName = a.JobType.Name,
                FieldName = a.Field.Name,
                AccountName = a.Account.Name
            }).ToListAsync(cancellationToken);

        return ReturnData<List<GetAssignmentsForListResponseDto>>.Success(assignments);
    }
}