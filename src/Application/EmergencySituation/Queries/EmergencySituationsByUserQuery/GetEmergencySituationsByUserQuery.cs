
using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.EmergencySituation.Queries.GetEmergencySituations;
using CleanArchitecture.Application.EmergencySituation.Queries.GetEmergencySituationsQuery;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.EmergencySituation.Queries.EmergencySituationsByUserQuery;
public class GetEmergencySituationsByUserQuery : IRequest<List<GetEmergencySituationsByUserResponseDto>>
{
    public Guid CreatedUser { get; set; }
}

public class GetEmergencySituationsByUserQueryHandler : IRequestHandler<GetEmergencySituationsByUserQuery, List<GetEmergencySituationsByUserResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetEmergencySituationsByUserQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<GetEmergencySituationsByUserResponseDto>> Handle(GetEmergencySituationsByUserQuery request, CancellationToken cancellationToken)
    {
        return await _context.EmergencySituations
            .Where(x => x.IsDeleted == false)
            .Where(y => y.CreatedUser == request.CreatedUser)
            .Select(a => _mapper.Map<GetEmergencySituationsByUserResponseDto>(a)).ToListAsync(cancellationToken);
    }
}
