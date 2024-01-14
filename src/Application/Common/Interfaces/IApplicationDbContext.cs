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
    DbSet<JobAssignment> JobAssignments { get; }
    DbSet<Notification> Notifications { get; }
    DbSet<NotificationReadRecord> NotificationReadRecords { get; }
    DbSet<Message> Messages { get; }
    DbSet<MessageReadRecord> MessageReadRecords { get; }
    DbSet<Domain.Entities.EmergencySituation> EmergencySituations { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
