using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Reports.Queries.GetDailyWorkConvexHull;
public record GetDailyWorkConvexHullQuery : IRequest<ReturnData<List<GetDailyWorkConvexHullQueryResponseDto>>>
{
    public Guid AccountId { get; set; }
    public Guid FieldId { get; set; }
    public DateTime WorkDate { get; set; }
}

public class GetDailyWorkConvexHullQueryHandler : IRequestHandler<GetDailyWorkConvexHullQuery, ReturnData<List<GetDailyWorkConvexHullQueryResponseDto>>>
{
    private readonly IApplicationDbContext _context;
    public GetDailyWorkConvexHullQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<List<GetDailyWorkConvexHullQueryResponseDto>>> Handle(GetDailyWorkConvexHullQuery request, CancellationToken cancellationToken)
    {
        var isAccountWorked = await _context.JobAssignments.AnyAsync(a => a.AccountId == request.AccountId && a.FieldId == request.FieldId && a.Date.Date == request.WorkDate.Date);
        List<List<double>> responseList = null;

        if (isAccountWorked)
        {
            responseList = new();
            List<Domain.Entities.LiveLocation> liveLocations = _context.LiveLocations.Where(a => a.AccountId == request.AccountId && a.CreatedDate.Date == request.WorkDate.Date).ToList();
            foreach (var item in liveLocations)
            {
                responseList.Add(new List<double> { item.Longitude, item.Latitude });
            }

        }

        var convexHull = ConvexHullWithQuichHull.printHull(responseList, responseList.Count);
        return ReturnData<List<GetDailyWorkConvexHullQueryResponseDto>>.Success(convexHull);
    }
}
