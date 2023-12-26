using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }
    DbSet<EntityHistory> EntityHistories { get; }
    DbSet<ErrorLog> ErrorLogs { get; }
    DbSet<Account> Accounts { get; }
    DbSet<Domain.Entities.JobType> JobTypes { get; }
    DbSet<Field> Fields { get; }
    DbSet<FieldCoordinate> FieldCoordinates { get; }
    DbSet<Domain.Entities.LiveLocation> LiveLocations { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
