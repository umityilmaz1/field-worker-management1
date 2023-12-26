using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.JobType.Commands.RemoveJobType;
public class RemoveJobTypeCommand : IRequest<ReturnData<bool>>
{
    public Guid Id { get; set; }
}

public class RemoveJobTypeCommandHandler : IRequestHandler<RemoveJobTypeCommand, ReturnData<bool>>
{
    private readonly IApplicationDbContext _context;
    public RemoveJobTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<bool>> Handle(RemoveJobTypeCommand request, CancellationToken cancellationToken)
    {
        var jobType = await _context.JobTypes.FindAsync(request.Id);
        if (jobType == null)
        {
            return ReturnData<bool>.Fail("Job type not found.");
        }
        jobType.IsActive = false;
        _context.JobTypes.Update(jobType);
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool>.Success(true);
    }
}
