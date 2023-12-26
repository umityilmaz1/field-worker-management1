using CleanArchitecture.Application.Accounts.Commands.AddAccount;
using CleanArchitecture.Application.Accounts.Commands.RemoveAccount;
using CleanArchitecture.Application.Accounts.Commands.UpdateAccount;
using CleanArchitecture.Application.Accounts.Queries.GetAccountById;
using CleanArchitecture.Application.Fields.Commands.AddField;
using CleanArchitecture.Application.Fields.Commands.RemoveField;
using CleanArchitecture.Application.Fields.Queries.GetFieldById;
using CleanArchitecture.Application.Fields.Queries.GetFields;
using CleanArchitecture.Application.LiveLocation.Commands;
using CleanArchitecture.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Model.Commons;

namespace WebApi.Controllers;
public class LiveLocationController : ApiControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> AddLocation(AddLiveLocationCommand command)
    {
        return ReturnData<bool>.Success(await Mediator.Send(command));
    }
}
