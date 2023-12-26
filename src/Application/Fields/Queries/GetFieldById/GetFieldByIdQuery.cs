using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Fields.Queries.GetFields;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Fields.Queries.GetFieldById;
public record GetFieldByIdQuery : IRequest<GetFieldByIdQueryResponseDto>
{
    public Guid Id { get; set; }
}

public class GetFieldByIdQueryHandler : IRequestHandler<GetFieldByIdQuery, GetFieldByIdQueryResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetFieldByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetFieldByIdQueryResponseDto> Handle(GetFieldByIdQuery request, CancellationToken cancellationToken)
    {
        var field = await _context.Fields.Include(a => a.Coordinates).FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        return _mapper.Map<GetFieldByIdQueryResponseDto>(field);
    }
}
