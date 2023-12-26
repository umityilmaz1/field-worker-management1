//using CleanArchitecture.Domain.Entities;
//using CleanArchitecture.Domain.Enums;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.EntityFrameworkCore.Diagnostics;

//namespace CleanArchitecture.Infrastructure.Persistence.Interceptors;

//public class AuditableEntityHistoryChangesInterceptor : SaveChangesInterceptor
//{
//    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
//    {
//        SetEntityHistories(eventData.Context);

//        return base.SavingChanges(eventData, result);
//    }

//    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
//    {
//        SetEntityHistories(eventData.Context);

//        return base.SavingChanges(eventData, result);
//    }

//    private void SetEntityHistories(DbContext? context)
//    {
//        if (context == null) return;

//        SetHistoryDefinitionForCreatedEntities(context, context.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added && entry.Entity.GetType() != typeof(EntityHistory)).ToList());
//        SetHistoryDefinitionForUpdatedEntities(context, context.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Modified && entry.Entity.GetType() != typeof(EntityHistory)).ToList());
//        SetHistoryDefinitionForDeletedEntities(context, context.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Deleted && entry.Entity.GetType() != typeof(EntityHistory)).ToList());
//        context.SaveChangesAsync();
//    }

//    private void SetHistoryDefinitionForCreatedEntities(DbContext? context, List<EntityEntry> addedEntities)
//    {
//        if (addedEntities.Any())
//        {
//            foreach (var entityEntry in addedEntities)
//            {
//                Guid transactionId = Guid.NewGuid();
//                foreach (var property in entityEntry.OriginalValues.Properties)
//                {
//                    context.Set<EntityHistory>().Add(new EntityHistory(Guid.Parse(entityEntry.Property("Id").CurrentValue.ToString()), entityEntry.Entity.GetType().Name, property.Name, GetPropertyType(entityEntry, property.Name), entityEntry.CurrentValues[property]?.ToString(), null, EntityHistoryChangeType.Create, transactionId));
//                }
//            }
//        }
//    }

//    private void SetHistoryDefinitionForUpdatedEntities(DbContext? context, List<EntityEntry> updatedEntities)
//    {
//        if (updatedEntities.Any())
//        {
//            foreach (var entityEntry in updatedEntities)
//            {
//                Guid transactionId = Guid.NewGuid();
//                foreach (var property in entityEntry.OriginalValues.Properties)
//                {
//                    context.Set<EntityHistory>().Add(new EntityHistory(Guid.Parse(entityEntry.Property("Id").CurrentValue.ToString()), entityEntry.Entity.GetType().Name, property.Name, GetPropertyType(entityEntry, property.Name), entityEntry.CurrentValues[property]?.ToString(), entityEntry.OriginalValues[property]?.ToString(), EntityHistoryChangeType.Update, transactionId));
//                }
//            }
//        }
//    }

//    private void SetHistoryDefinitionForDeletedEntities(DbContext? context, List<EntityEntry> updatedEntities)
//    {
//        if (updatedEntities.Any())
//        {
//            foreach (var entityEntry in updatedEntities)
//            {
//                Guid transactionId = Guid.NewGuid();
//                foreach (var property in entityEntry.OriginalValues.Properties)
//                {
//                    context.Set<EntityHistory>().Add(new EntityHistory(Guid.Parse(entityEntry.Property("Id").CurrentValue.ToString()), entityEntry.Entity.GetType().Name, property.Name, GetPropertyType(entityEntry, property.Name), null, entityEntry.OriginalValues[property]?.ToString(), EntityHistoryChangeType.Delete, transactionId));
//                }
//            }
//        }
//    }

//    private string GetPropertyType(EntityEntry entry, string propertyName)
//    {
//        var propertyInfo = entry.Entity.GetType().GetProperty(propertyName);
//        if (propertyInfo != null)
//        {
//            var propertyType = propertyInfo.PropertyType;
//            return propertyType.FullName;
//        }
//        return null;
//    }
//}