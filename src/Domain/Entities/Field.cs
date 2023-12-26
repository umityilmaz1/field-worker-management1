using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class Field : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<FieldCoordinate> Coordinates { get; set; }
}
