using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class JobType : BaseAuditableEntity
{
    public string Name { get; set; }

    public virtual ICollection<Account> Accounts { get; set; }
}
