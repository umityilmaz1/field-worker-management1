using CleanArchitecture.Application.Accounts.Commands.AddAccount;
using CleanArchitecture.Application.Accounts.Commands.RemoveAccount;
using CleanArchitecture.Application.Accounts.Commands.UpdateAccount;
using CleanArchitecture.Application.Accounts.Queries.GetAccountById;
using CleanArchitecture.Application.Accounts.Queries.GetAccounts;
using CleanArchitecture.Application.Fields.Queries.GetFields;
using CleanArchitecture.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Model.Commons;

namespace WebApi.Controllers;
public class AccountController : ApiControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> AddAccount(AddAccountCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> UpdateAccount(UpdateAccountCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> RemoveAccount(RemoveAccountCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ReturnData<List<GetAccountsResponseDto>>> GetAllAccounts()
    {
        return ReturnData<List<GetAccountsResponseDto>>.Success(await Mediator.Send(new GetAccountsQuery()));
    }

    [HttpGet]
    [Route("[action]/{Id}")]
    public async Task<ReturnData<GetAccountByIdQueryResponseDto>> GetAccountById([FromRoute]GetAccountByIdQuery query)
    {
        return await Mediator.Send(query);
    }
}
