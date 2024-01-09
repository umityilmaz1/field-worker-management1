using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.NotificationSystem.Queries.GetNotifications;
public record GetNotificatiosQuery : IRequest<ReturnData<List<GetNotificationsQueryResponseDto>>>
{
    public Guid AccountId { get; set; }
}

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificatiosQuery, ReturnData<List<GetNotificationsQueryResponseDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetNotificationsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<List<GetNotificationsQueryResponseDto>>> Handle(GetNotificatiosQuery request, CancellationToken cancellationToken)
    {
        var notifications = await _context.Notifications
            //.Where(x => x.NotificationReadRecords.Any(y => y.AccountId == request.AccountId))
            .Select(x => new GetNotificationsQueryResponseDto
            {
                NotificationId = x.Id,
                Content = x.Content,
                SendDate = x.CreatedDate,
                IsRead = x.NotificationReadRecords.Any(y => y.AccountId == request.AccountId)
            })
            .ToListAsync(cancellationToken);

        return ReturnData<List<GetNotificationsQueryResponseDto>>.Success(notifications);
    }
}
