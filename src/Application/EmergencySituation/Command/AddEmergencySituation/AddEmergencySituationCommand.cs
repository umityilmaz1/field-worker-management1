using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Model.Commons;
using MediatR;

namespace CleanArchitecture.Application.EmergencySituation.Command.AddEmergencySituation;
public record AddEmergencySituationCommand : IRequest<ReturnData<bool>>
{
    public EmergencyType EmergencyType { get; set; }
    public string Description { get; set; }
    public Guid CreatedUser { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}

public class AddEmergencySituationCommandHandler : IRequestHandler<AddEmergencySituationCommand, ReturnData<bool>>
{
    private readonly IApplicationDbContext _context;
    public AddEmergencySituationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ReturnData<bool>> Handle(AddEmergencySituationCommand request, CancellationToken cancellationToken)
    {
        _context.EmergencySituations.Add(new Domain.Entities.EmergencySituation
        {
            EmergencyType = request.EmergencyType,
            Description = request.Description,
            CreatedUser = request.CreatedUser,
            Longitude = request.Longitude,
            Latitude = request.Latitude,
            CreatedDate = DateTime.Now
        });

        var entity = new Domain.Entities.Notification
        {
            Content = request.EmergencyType.ToString() + request.Description,
            IsEmergency = true
        };
        _context.Notifications.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return ReturnData<bool>.Success(true);
    }
}
