
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Model.Commons;
using MediatR;

namespace CleanArchitecture.Application.EmergencySituation.Command.UpdateEmergencySituation;
public record UpdateEmergencySituationCommand : IRequest<ReturnData<bool>>
{
    public Guid Id { get; set; }
    public EmergencyType EmergencyType { get; set; }
    public string Description { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}

public class UpdateEmergencySituationCommandHandler : IRequestHandler<UpdateEmergencySituationCommand, ReturnData<bool>>
{
    private readonly IApplicationDbContext _context;
    public UpdateEmergencySituationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<bool>> Handle(UpdateEmergencySituationCommand request, CancellationToken cancellationToken)
    {
        var emergency = await _context.EmergencySituations.FindAsync(request.Id);
        if (emergency == null)
        {
            return ReturnData<bool>.Fail("Emergency Situations not found.");
        }
        emergency.EmergencyType = request.EmergencyType;
        emergency.Description = request.Description;
        emergency.Longitude = request.Longitude;
        emergency.Latitude = request.Latitude;
        _context.EmergencySituations.Update(emergency);
        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool>.Success(true);
    }
}
