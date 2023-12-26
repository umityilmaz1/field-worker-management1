using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Fields.Commands.UpdateField;
public record UpdateFieldCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Coordinate2> Coordinates { get; set; }

    public class Coordinate2
    {
        public int Order { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}

public class UpdateFieldCommandHandler : IRequestHandler<UpdateFieldCommand, bool>
{
    private readonly IApplicationDbContext _context;
    public UpdateFieldCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(UpdateFieldCommand request, CancellationToken cancellationToken)
    {
        Field field = await _context.Fields.Include(a => a.Coordinates).FirstOrDefaultAsync(a => a.Id == request.Id);
        if (field == null)
        {
            return false;
        }
        field.Name = request.Name;
        field.Description = request.Description;
        field.Coordinates = request.Coordinates.Select(a => new FieldCoordinate() { Order = a.Order, Longitude = a.Longitude, Latitude = a.Latitude }).ToList();
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
