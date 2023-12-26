using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArchitecture.Infrastructure.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public AuditableEntitySaveChangesInterceptor(
        ICurrentUserService currentUserService,
        IDateTime dateTime)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        SetEntityHistories(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        SetEntityHistories(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUserService.UserId?.ToString();
                entry.Entity.CreatedDate = _dateTime.Now;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = _currentUserService.UserId?.ToString();
                entry.Entity.LastModifiedDate = _dateTime.Now;
            }
        }
    }


    private void SetEntityHistories(DbContext? context)
    {
        try
        {
            SetHistoryDefinitionForCreatedEntities(context, context.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added && entry.Entity.GetType() != typeof(EntityHistory)).ToList());
            SetHistoryDefinitionForUpdatedEntities(context, context.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Modified && entry.Entity.GetType() != typeof(EntityHistory)).ToList());
            SetHistoryDefinitionForDeletedEntities(context, context.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Deleted && entry.Entity.GetType() != typeof(EntityHistory)).ToList());
        }
        catch (Exception ex)
        {
            //UMIT: burada exception yerine log atılmalı
            //throw new EntityHistoryException(ex);
        }        
    }

    private void SetHistoryDefinitionForCreatedEntities(DbContext? context, List<EntityEntry> addedEntities)
    {
        if (addedEntities.Any())
        {
            foreach (var entityEntry in addedEntities)
            {
                Guid transactionId = Guid.NewGuid();
                foreach (var property in entityEntry.OriginalValues.Properties)
                {
                    context.Set<EntityHistory>().Add(new EntityHistory(Guid.Parse(entityEntry.Property("Id").CurrentValue.ToString()), entityEntry.Entity.GetType().Name, property.Name, GetPropertyType(entityEntry, property.Name), entityEntry.CurrentValues[property]?.ToString(), null, EntityHistoryChangeType.Create, transactionId, _dateTime.Now, _currentUserService.UserId?.ToString()));
                }
            }
        }
    }

    private void SetHistoryDefinitionForUpdatedEntities(DbContext? context, List<EntityEntry> updatedEntities)
    {
        if (updatedEntities.Any())
        {
            foreach (var entityEntry in updatedEntities)
            {
                Guid transactionId = Guid.NewGuid();
                foreach (var property in entityEntry.OriginalValues.Properties)
                {
                    var originalValue = entityEntry.OriginalValues[property];
                    var currentValue = entityEntry.CurrentValues[property];

                    if (!object.Equals(originalValue, currentValue))
                    { 
                        context.Set<EntityHistory>().Add(new EntityHistory(Guid.Parse(entityEntry.Property("Id").CurrentValue.ToString()), entityEntry.Entity.GetType().Name, property.Name, GetPropertyType(entityEntry, property.Name), entityEntry.CurrentValues[property]?.ToString(), entityEntry.OriginalValues[property]?.ToString(), EntityHistoryChangeType.Update, transactionId, _dateTime.Now, _currentUserService.UserId?.ToString()));
                    }
                }
            }
        }
    }

    private void SetHistoryDefinitionForDeletedEntities(DbContext? context, List<EntityEntry> updatedEntities)
    {
        if (updatedEntities.Any())
        {
            foreach (var entityEntry in updatedEntities)
            {
                Guid transactionId = Guid.NewGuid();
                foreach (var property in entityEntry.OriginalValues.Properties)
                {
                    var originalValue = entityEntry.OriginalValues[property];
                    var currentValue = entityEntry.CurrentValues[property];

                    if (!object.Equals(originalValue, currentValue))
                    { 
                        context.Set<EntityHistory>().Add(new EntityHistory(Guid.Parse(entityEntry.Property("Id").CurrentValue.ToString()), entityEntry.Entity.GetType().Name, property.Name, GetPropertyType(entityEntry, property.Name), null, entityEntry.OriginalValues[property]?.ToString(), EntityHistoryChangeType.Delete, transactionId, _dateTime.Now, _currentUserService.UserId?.ToString()));
                    }
                }
            }
        }
    }

    private string GetPropertyType(EntityEntry entry, string propertyName)
    {
        var propertyInfo = entry.Entity.GetType().GetProperty(propertyName);
        if (propertyInfo != null)
        {
            var propertyType = propertyInfo.PropertyType;
            return propertyType.FullName;
        }
        return null;
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}