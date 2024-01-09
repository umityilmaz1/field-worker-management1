using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Model.Commons;
using MediatR;

namespace CleanArchitecture.Application.MessageSystem.Commands.CreateMessage;
public record CreateMessageCommand : IRequest<ReturnData<bool?>>
{
    public Guid ReceiverId { get; set; }
    public string Content { get; set; }
}

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, ReturnData<bool?>>
{
    private readonly IApplicationDbContext _context;

    public CreateMessageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReturnData<bool?>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new Message
        {
            ReceiverId = request.ReceiverId,
            Content = request.Content
        };

        _context.Messages.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool?>.Success(true);
    }
}