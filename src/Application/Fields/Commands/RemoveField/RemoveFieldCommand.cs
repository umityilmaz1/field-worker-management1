using CleanArchitecture.Application.Common.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Fields.Commands.RemoveField;
public record RemoveFieldCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class RemoveFieldCommandHandler : IRequestHandler<RemoveFieldCommand, bool>
{
    private readonly IApplicationDbContext _context;
    public RemoveFieldCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(RemoveFieldCommand request, CancellationToken cancellationToken)
    {
        var field = await _context.Fields.FindAsync(request.Id);
        if (field == null)
            return false;
        field.IsDeleted = true;
        _context.Fields.Update(field);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
