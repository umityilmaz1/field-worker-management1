using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities;
public class EmergencySituation : BaseAuditableEntity
{
    public EmergencyType EmergencyType { get; set; }
    public string Description { get; set; }
    public Guid CreatedUser { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}
