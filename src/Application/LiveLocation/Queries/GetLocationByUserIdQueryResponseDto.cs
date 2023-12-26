using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.LiveLocation.Queries;
public class GetLocationByUserIdQueryResponseDto
{
    public Guid AccountId { get; set; }

    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public DateTime CreatedDate { get; set; }
}
