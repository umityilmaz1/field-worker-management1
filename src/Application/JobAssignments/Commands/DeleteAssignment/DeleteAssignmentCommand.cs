using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.JobAssignments.Commands.DeleteAssignment;
public record DeleteAssignmentCommand : IRequest<ReturnData<bool?>>
{
    public Guid Id { get; set; }
}

public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand, ReturnData<bool?>>
{
    private readonly IApplicationDbContext _context;
    public DeleteAssignmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<bool?>> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        var jobAssignment = await _context.JobAssignments.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (jobAssignment == null)
        {
            return ReturnData<bool?>.Fail("Assignment not found");
        }

        jobAssignment.IsDeleted = true;
        _context.JobAssignments.Update(jobAssignment);
        await _context.SaveChangesAsync(cancellationToken);

        return ReturnData<bool?>.Success();
    }
}