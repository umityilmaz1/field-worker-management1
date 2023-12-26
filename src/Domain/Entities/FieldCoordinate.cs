using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class FieldCoordinate : BaseAuditableEntity
{
    public int Order { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }

    public Guid FieldId { get; set; }
    public virtual Field Field { get; set; }
}
