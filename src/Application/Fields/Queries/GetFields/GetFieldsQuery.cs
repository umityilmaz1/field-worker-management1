using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Fields.Queries.GetFields;
public record GetAllFieldsQuery : IRequest<List<GetFieldsQueryResponseDto>>
{
}

public class GetAllFieldsQueryHandler : IRequestHandler<GetAllFieldsQuery, List<GetFieldsQueryResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllFieldsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<GetFieldsQueryResponseDto>> Handle(GetAllFieldsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Fields
            .Select(a => _mapper.Map<GetFieldsQueryResponseDto>(a))
            .ToListAsync(cancellationToken);
    }
}