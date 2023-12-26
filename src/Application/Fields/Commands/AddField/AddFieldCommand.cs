using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Fields.Commands.AddField;
public record AddFieldCommand : IRequest<bool>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Coordinate> Coordinates { get; set; }

    public class Coordinate
    {
        public int Order { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}

public class AddFieldCommandHandler : IRequestHandler<AddFieldCommand, bool>
{
    private readonly IApplicationDbContext _context;
    public AddFieldCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(AddFieldCommand request, CancellationToken cancellationToken)
    {
        Field field = new()
        {
            Name = request.Name,
            Description = request.Description,
            Coordinates = request.Coordinates.Select(a => new FieldCoordinate() { Order = a.Order, Longitude = a.Longitude, Latitude = a.Latitude }).ToList()
        };
        _context.Fields.Add(field);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
