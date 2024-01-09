using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.NotificationSystem.Commands.SetReadedNotification;
public record SetReadedNotificationCommand : IRequest<ReturnData<bool?>>
{
    public Guid NotificationId { get; set; }
    public Guid AccountId { get; set; }
}

public class SetReadedNotificationCommandHandler : IRequestHandler<SetReadedNotificationCommand, ReturnData<bool?>>
{
    private readonly IApplicationDbContext _context;

    public SetReadedNotificationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<bool?>> Handle(SetReadedNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.FindAsync(request.NotificationId);
        if (entity == null)
        {
            return ReturnData<bool?>.Fail("Notification not found");
        }

        NotificationReadRecord notificationReadRecord = new()
        {
            NotificationId = request.NotificationId,
            AccountId = request.AccountId
        };

        _context.NotificationReadRecords.Add(notificationReadRecord);
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool?>.Success(true);
    }
}