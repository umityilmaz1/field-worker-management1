using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class LiveLocation : BaseAuditableEntity
{
    public Guid AccountId { get; set; }

    public virtual Account Account { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}
