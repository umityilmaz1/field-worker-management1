using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Model.Commons;
using MediatR;

namespace CleanArchitecture.Application.EmergencySituation.Command.RemoveEmergencySituation;
public record RemoveEmergencySituationCommand : IRequest<ReturnData<bool>>
{
    public Guid Id { get; set; }
}

public class RemoveEmergencySituationCommandHandler : IRequestHandler<RemoveEmergencySituationCommand, ReturnData<bool>>
{
    private readonly IApplicationDbContext _context;
    public RemoveEmergencySituationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<bool>> Handle(RemoveEmergencySituationCommand request, CancellationToken cancellationToken)
    {
        var emergency = await _context.EmergencySituations.FindAsync(request.Id);
        if (emergency == null)
        {
            return ReturnData<bool>.Fail("Emergency Situations not found.");
        }
        emergency.IsDeleted = true;
        _context.EmergencySituations.Update(emergency);
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool>.Success(true);
    }
}
