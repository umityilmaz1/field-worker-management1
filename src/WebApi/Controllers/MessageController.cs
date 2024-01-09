using CleanArchitecture.Application.MessageSystem.Commands.CreateMessage;
using CleanArchitecture.Application.MessageSystem.Commands.SetReadedMessage;
using CleanArchitecture.Application.MessageSystem.Queries.GetMessages;
using CleanArchitecture.Model.Commons;
using CleanArchitecture.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class MessageController : ApiControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool?>> CreateMessage(CreateMessageCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool?>> SetReadMessage(SetReadedMessageCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    [Route("[action]/{AccountId}")]
    public async Task<ReturnData<List<GetMessagesQueryResponseDto>>> GetMessages([FromRoute] GetMessagesQuery query)
    {
        return await Mediator.Send(query);
    }
}
