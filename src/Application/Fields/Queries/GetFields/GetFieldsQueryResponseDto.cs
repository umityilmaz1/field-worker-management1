using AutoMapper;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Fields.Queries.GetFields;
public class GetFieldsQueryResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Field, GetFieldsQueryResponseDto>();
        }
    }
}
