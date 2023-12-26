using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.JobType.Commands.AddJobType;
public class AddJobTypeCommand : IRequest<ReturnData<bool>>
{
    public string Name { get; set; }
}

public class AddJobTypeCommandHandler : IRequestHandler<AddJobTypeCommand, ReturnData<bool>>
{
    private readonly IApplicationDbContext _context;
    public AddJobTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<bool>> Handle(AddJobTypeCommand request, CancellationToken cancellationToken)
    {
        _context.JobTypes.Add(new Domain.Entities.JobType
        {
            Name = request.Name,
        });
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool>.Success(true);
    }
}
