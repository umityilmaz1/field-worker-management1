using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.JobType.Queries.GetAll;
public record GetAllQuery : IRequest<ReturnData<List<GetAllResponseDto>>>
{
}

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, ReturnData<List<GetAllResponseDto>>>
{
    private readonly IApplicationDbContext _context;
    public GetAllQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<List<GetAllResponseDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var jobTypes = await _context.JobTypes.Select(a => new GetAllResponseDto
        {
            Id = a.Id,
            Name = a.Name
        }).ToListAsync(cancellationToken);

        return ReturnData<List<GetAllResponseDto>>.Success(jobTypes);
    }
}