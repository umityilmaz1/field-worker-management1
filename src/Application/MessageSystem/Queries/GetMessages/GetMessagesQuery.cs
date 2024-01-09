using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Model.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.MessageSystem.Queries.GetMessages;
public record GetMessagesQuery : IRequest<ReturnData<List<GetMessagesQueryResponseDto>>>
{
    public Guid AccountId { get; set; }
}

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, ReturnData<List<GetMessagesQueryResponseDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetMessagesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<List<GetMessagesQueryResponseDto>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await _context.Messages
            .Where(a => a.ReceiverId == request.AccountId)
            //.Where(x => x.MessageReadRecords.Any(y => y.AccountId == request.AccountId))
            .Select(x => new GetMessagesQueryResponseDto
            {
                MessageId = x.Id,
                Content = x.Content,
                SendDate = x.CreatedDate,
                IsRead = x.MessageReadRecords.Any(y => y.AccountId == request.AccountId)
            })
            .ToListAsync(cancellationToken);

        return ReturnData<List<GetMessagesQueryResponseDto>>.Success(messages);
    }
}