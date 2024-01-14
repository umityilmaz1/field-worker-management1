using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.EmergencySituation.Queries.EmergencySituationsByUserQuery;
public class GetEmergencySituationsByUserResponseDto
{
    public Guid Id { get; set; }
    public EmergencyType EmergencyType { get; set; }
    public string Description { get; set; }
    public Guid CreatedUser { get; set; }
}
