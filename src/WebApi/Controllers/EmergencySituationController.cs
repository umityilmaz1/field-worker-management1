using CleanArchitecture.Application.Accounts.Queries.GetAccountById;
using CleanArchitecture.Application.Accounts.Queries.GetAccounts;
using CleanArchitecture.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Model.Commons;
using CleanArchitecture.Application.EmergencySituation.Command.AddEmergencySituation;
using CleanArchitecture.Application.EmergencySituation.Command.UpdateEmergencySituation;
using CleanArchitecture.Application.EmergencySituation.Command.RemoveEmergencySituation;
using CleanArchitecture.Application.EmergencySituation.Queries.GetEmergencySituations;
using CleanArchitecture.Application.EmergencySituation.Queries.GetEmergencySituationsQuery;
using CleanArchitecture.Application.EmergencySituation.Queries.EmergencySituationsByUserQuery;

namespace WebApi.Controllers;
public class EmergencySituationController : ApiControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> AddEmergencySituation(AddEmergencySituationCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> UpdateEmergencySituation(UpdateEmergencySituationCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> RemoveEmergencySituation(RemoveEmergencySituationCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ReturnData<List<GetEmergencySituationsResponseDto>>> GetAllEmergencySituations()
    {
        return ReturnData<List<GetEmergencySituationsResponseDto>>.Success(await Mediator.Send(new GetEmergencySituationQuery()));
    }

    //[HttpGet]
    //[Route("[action]/{Id}")]
    //public async Task<ReturnData<GetEmergencySituationsByUserResponseDto>> GetEmergencySituationsByUser([FromRoute] GetEmergencySituationsByUserQuery query)
    //{
    //    return ReturnData<List<GetEmergencySituationsByUserResponseDto>>.Success(await Mediator.Send(query));
    //}
}
