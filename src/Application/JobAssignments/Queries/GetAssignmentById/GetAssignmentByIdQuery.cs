using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.JobAssignments.Queries.GetAssignmentById;
public record GetAssignmentByIdQuery : IRequest<ReturnData<GetAssignmentByIdResponseDto>>
{
    public Guid Id { get; set; }
}

public class GetAssignmentByIdQueryHandler : IRequestHandler<GetAssignmentByIdQuery, ReturnData<GetAssignmentByIdResponseDto>>
{
    private readonly IApplicationDbContext _context;
    public GetAssignmentByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<GetAssignmentByIdResponseDto>> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var assignment = await _context.JobAssignments
            .Include(a => a.JobType)
            .Include(a => a.Field)
            .Include(a => a.Account)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (assignment == null)
        {
            return ReturnData<GetAssignmentByIdResponseDto>.Fail("Assignment not found");
        }

        var response = new GetAssignmentByIdResponseDto
        {
            AccountId = assignment.AccountId,
            JobTypeId = assignment.JobTypeId,
            FieldId = assignment.FieldId,
            Date = assignment.Date,
            JobTypeName = assignment.JobType.Name,
            FieldName = assignment.Field.Name,
            AccountName = assignment.Account.Name
        };

        return ReturnData<GetAssignmentByIdResponseDto>.Success(response);
    }
}