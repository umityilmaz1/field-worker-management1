using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Model.Commons;
using MediatR;

namespace CleanArchitecture.Application.MessageSystem.Commands.SetReadedMessage;
public record SetReadedMessageCommand : IRequest<ReturnData<bool?>>
{
    public Guid MessageId { get; set; }
    public Guid AccountId { get; set; }
}

public class SetReadedMessageCommandHandler : IRequestHandler<SetReadedMessageCommand, ReturnData<bool?>>
{
    private readonly IApplicationDbContext _context;

    public SetReadedMessageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<bool?>> Handle(SetReadedMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Messages.FindAsync(request.MessageId);
        if (entity == null)
        {
            return ReturnData<bool?>.Fail("Message not found");
        }

        MessageReadRecord messageReadRecord = new()
        {
            MessageId = request.MessageId,
            AccountId = request.AccountId
        };

        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool?>.Success(true);
    }
}