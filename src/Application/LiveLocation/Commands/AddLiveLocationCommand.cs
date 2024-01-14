using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Fields.Commands.AddField;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.LiveLocation.Commands;
public class AddLiveLocationCommand : IRequest<bool>
{
    public Guid AccountId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public DateTime CreateDate { get; set; }
}

public class AddLiveLocationCommandHandler : IRequestHandler<AddLiveLocationCommand, bool>
{
    private readonly IApplicationDbContext _context;
    public AddLiveLocationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(AddLiveLocationCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.LiveLocation liveLocation = new()
        {
            AccountId = request.AccountId,
            Longitude = request.Longitude,
            Latitude = request.Latitude,
            CreatedDate = DateTime.Now
        };
        _context.LiveLocations.Add(liveLocation);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
