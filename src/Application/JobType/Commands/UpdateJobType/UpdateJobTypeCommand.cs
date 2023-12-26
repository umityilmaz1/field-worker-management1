using System;

using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.JobType.Commands.UpdateJobType;
public class UpdateJobTypeCommand : IRequest<ReturnData<bool>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Account> Accounts { get; set; }
}

public class UpdateJobTypeCommandHandler : IRequestHandler<UpdateJobTypeCommand, ReturnData<bool>>
{
    private readonly IApplicationDbContext _context;
    public UpdateJobTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<bool>> Handle(UpdateJobTypeCommand request, CancellationToken cancellationToken)
    {
        var jobType = await _context.JobTypes.FindAsync(request.Id);
        if (jobType == null)
        {
            return ReturnData<bool>.Fail("Job type not found.");
        }
        jobType.Name = request.Name;
        jobType.Accounts = request.Accounts;
        _context.JobTypes.Update(jobType);
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool>.Success(true);
    }
}
