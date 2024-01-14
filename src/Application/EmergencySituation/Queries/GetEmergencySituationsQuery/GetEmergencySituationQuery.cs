using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.EmergencySituation.Queries.GetEmergencySituationsQuery;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.EmergencySituation.Queries.GetEmergencySituations;
public class GetEmergencySituationQuery : IRequest<List<GetEmergencySituationsResponseDto>>
{
}

public class GetEmergencySituationQueryHandler : IRequestHandler<GetEmergencySituationQuery, List<GetEmergencySituationsResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetEmergencySituationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<GetEmergencySituationsResponseDto>> Handle(GetEmergencySituationQuery request, CancellationToken cancellationToken)
    {
        return await _context.EmergencySituations
            .Where(x => x.IsDeleted == false)
            .Select(a => _mapper.Map<GetEmergencySituationsResponseDto>(a))
            .ToListAsync(cancellationToken);
    }
}
