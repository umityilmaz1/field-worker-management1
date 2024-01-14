using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
