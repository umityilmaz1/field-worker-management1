using CleanArchitecture.Application.Accounts.Commands.AddAccount;
using CleanArchitecture.Application.Accounts.Commands.RemoveAccount;
using CleanArchitecture.Application.Accounts.Commands.UpdateAccount;
using CleanArchitecture.Application.Accounts.Queries.GetAccountById;
using CleanArchitecture.Application.Fields.Commands.AddField;
using CleanArchitecture.Application.Fields.Commands.RemoveField;
using CleanArchitecture.Application.Fields.Commands.UpdateField;
using CleanArchitecture.Application.Fields.Queries.GetFieldById;
using CleanArchitecture.Application.Fields.Queries.GetFields;
using CleanArchitecture.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Model.Commons;

namespace WebApi.Controllers;
public class FieldController : ApiControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool>> AddField(AddFieldCommand command)
    {
        return ReturnData<bool>.Success(await Mediator.Send(command));
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<ReturnData<bool>> UpdateAccount(UpdateFieldCommand command)
    {
        return ReturnData<bool>.Success(await Mediator.Send(command));
    }

    [HttpPut]
    [Route("[action]/{id}")]
    public async Task<ReturnData<bool>> RemoveField([FromRoute]RemoveFieldCommand command)
    {
        return ReturnData<bool>.Success(await Mediator.Send(command));
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<ReturnData<GetFieldByIdQueryResponseDto>> GetFieldById([FromRoute]GetFieldByIdQuery query)
    {
        return ReturnData<GetFieldByIdQueryResponseDto>.Success(await Mediator.Send(query));
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ReturnData<List<GetFieldsQueryResponseDto>>> GetAllFields()
    {
        return ReturnData<List<GetFieldsQueryResponseDto>>.Success(await Mediator.Send(new GetAllFieldsQuery()));
    }
}
