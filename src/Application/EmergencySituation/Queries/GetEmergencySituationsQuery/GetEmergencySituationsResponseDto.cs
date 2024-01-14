using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Accounts.Queries.GetAccounts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.EmergencySituation.Queries.GetEmergencySituationsQuery;
public class GetEmergencySituationsResponseDto
{
    public Guid Id { get; set; }
    public EmergencyType EmergencyType { get; set; }
    public string Description { get; set; }
    public Guid CreatedUser { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.EmergencySituation, GetEmergencySituationsResponseDto>();
        }
    }
}
