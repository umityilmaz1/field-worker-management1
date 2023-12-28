using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.JobAssignments.Commands.UpdateAssignment;
public record UpdateAssignmentCommand : IRequest<ReturnData<bool?>>
{
    public Guid AssignmentId { get; set; }
    public Guid JobTypeId { get; set; }
    public Guid FieldId { get; set; }
    public DateTime Date { get; set; }
}

public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand, ReturnData<bool?>>
{
    private readonly IApplicationDbContext _context;
    public UpdateAssignmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<bool?>> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var jobAssignment = await _context.JobAssignments.FirstOrDefaultAsync(a => a.Id == request.AssignmentId, cancellationToken);
        if (jobAssignment == null)
        {
            return ReturnData<bool?>.Fail("Assignment not found");
        }

        jobAssignment.JobTypeId = request.JobTypeId;
        jobAssignment.FieldId = request.FieldId;
        jobAssignment.Date = request.Date;

        await _context.SaveChangesAsync(cancellationToken);

        return ReturnData<bool?>.Success();
    }
}