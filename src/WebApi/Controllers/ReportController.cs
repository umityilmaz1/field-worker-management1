using CleanArchitecture.Application.Reports.Queries.GetDailyWorkConvexHull;
using CleanArchitecture.Model.Commons;
using CleanArchitecture.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class ReportController : ApiControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<List<GetDailyWorkConvexHullQueryResponseDto>>> GetDailyWorkingLocations(GetDailyWorkConvexHullQuery command)
    {
        return await Mediator.Send(command);
    }
}
