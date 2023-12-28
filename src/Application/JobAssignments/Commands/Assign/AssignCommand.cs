using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Model.Commons;
using MediatR;

namespace CleanArchitecture.Application.JobAssignments.Commands.Assign;
public record AssignCommand : IRequest<ReturnData<bool?>>
{
    public Guid AccountId { get; set; }
    public Guid JobTypeId { get; set; }
    public Guid FieldId { get; set; }
    public DateTime Date { get; set; }
}

public class AssignCommandHandler : IRequestHandler<AssignCommand, ReturnData<bool?>>
{
    private readonly IApplicationDbContext _context;
    public AssignCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<bool?>> Handle(AssignCommand request, CancellationToken cancellationToken)
    {
        var jobAssignment = new JobAssignment
        {
            AccountId = request.AccountId,
            JobTypeId = request.JobTypeId,
            FieldId = request.FieldId,
            Date = request.Date
        };

        await _context.JobAssignments.AddAsync(jobAssignment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ReturnData<bool?>.Success();
    }
}
