using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Accounts.Queries.GetAccountById;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Model.Commons;

namespace CleanArchitecture.Application.LiveLocation.Queries;
//public class GetLocationByUserIdQuery : IRequest<ReturnData<GetLocationByUserIdQueryResponseDto>>
//{
//    public Guid UserId { get; set; }
//}

//public class GetLocationByUserIdQueryHandler : IRequestHandler<GetLocationByUserIdQuery, ReturnData<GetLocationByUserIdQueryResponseDto>>
//{
//    private readonly IApplicationDbContext _context;
//    public GetLocationByUserIdQueryHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<ReturnData<GetLocationByUserIdQueryResponseDto>> Handle(GetLocationByUserIdQuery request, CancellationToken cancellationToken)
//    {
//        var location = _context.LiveLocations.FirstOrDefaultAsync(a => a.AccountId == request.UserId).Result;

//        if (location == null)
//        {
//            return ReturnData<GetLocationByUserIdQueryResponseDto>.Fail("Location not found.");
//        }

//        return ReturnData<GetLocationByUserIdQueryResponseDto>.Success(new GetLocationByUserIdQueryResponseDto
//        {
//            AccountId = location.AccountId,
//            Longitude = location.Longitude,
//            Latitude = location.Latitude,

//        });
//    }
//}
