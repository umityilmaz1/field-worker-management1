using CleanArchitecture.Application.Fields.Commands.AddField;
using CleanArchitecture.Application.Fields.Commands.RemoveField;
using CleanArchitecture.Application.Fields.Commands.UpdateField;
using CleanArchitecture.Application.NotificationSystem.Commands.CreateNotification;
using CleanArchitecture.Application.NotificationSystem.Commands.SetReadedNotification;
using CleanArchitecture.Application.NotificationSystem.Queries.GetNotifications;
using CleanArchitecture.Model.Commons;
using CleanArchitecture.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers;
public class NotificationController : ApiControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool?>> CreateNotification(CreateNotificationCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ReturnData<bool?>> SetReadNotification(SetReadedNotificationCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    [Route("[action]/{AccountId}")]
    public async Task<ReturnData<List<GetNotificationsQueryResponseDto>>> GetNotifications([FromRoute] GetNotificatiosQuery query)
    {
        return await Mediator.Send(query);
    }
}
