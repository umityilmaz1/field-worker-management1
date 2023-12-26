using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Accounts.Queries.GetAccountById;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.Accounts.Queries.GetAccounts;
public class GetAccountsQuery : IRequest<List<GetAccountsResponseDto>>
{
}

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<GetAccountsResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAccountsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<GetAccountsResponseDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .Select(a => _mapper.Map<GetAccountsResponseDto>(a))
            .ToListAsync(cancellationToken);
    }
}
