using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.JobType.Queries.GetJobTypeById;
public class GetJobTypeByIdQueryResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Account> Accounts { get; set; }
}
