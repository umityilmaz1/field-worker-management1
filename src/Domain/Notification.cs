using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain;
public class Notification : BaseAuditableEntity
{
    public Guid AccountId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
