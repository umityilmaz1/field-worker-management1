using AutoMapper;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Fields.Queries.GetFields;
public class GetFieldByIdQueryResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Coordinate1> Coordinates { get; set; }
    public class Coordinate1
    {
        public int Order { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Field, GetFieldByIdQueryResponseDto>().ForMember(d => d.Coordinates,
                opt => opt.MapFrom(s => s.Coordinates.Select(a => new Coordinate1 { Order = a.Order, Longitude = a.Longitude, Latitude = a.Latitude })));
        }
    }
}