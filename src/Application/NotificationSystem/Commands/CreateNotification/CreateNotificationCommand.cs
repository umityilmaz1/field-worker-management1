using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Model.Commons;
using MediatR;

namespace CleanArchitecture.Application.NotificationSystem.Commands.CreateNotification;
public record CreateNotificationCommand : IRequest<ReturnData<bool?>>
{
    public string Content { get; set; }
}

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, ReturnData<bool?>>
{
    private readonly IApplicationDbContext _context;

    public CreateNotificationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<bool?>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = new Notification
        {
            Content = request.Content
        };

        _context.Notifications.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool?>.Success(true);
    }
}