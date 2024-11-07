using GenericRepository.Application.Interfaces.UnitOfWork;
using GenericRepository.Domain;
using GenericRepository.Domain.Entities;
using GenericRepository.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GenericRepository.Infrastructure.EfInterceptors;

public sealed class AddOutboxMessageInterceptor(IUnitOfWork<CommandContext> unitOfWork) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if (context != null)
        {
            BeforeSave(context);
        }
        
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
      
        if (context != null)
        {
            BeforeSave(context);
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void BeforeSave(DbContext context)
    {
        var domainEvents = GetDomainEvents(context);
        
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            var outboxMessage = OutBoxMessage.CreateByDomainEvent(domainEvent);
            unitOfWork.OutboxMessages.Add(outboxMessage);
        }

        unitOfWork.Save();
    }
    private static List<IDomainEvent> GetDomainEvents(DbContext context)
    {
        var domainEvents = context
            .ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .Where(e=> e.DomainEvents.Any())
            .SelectMany(entity =>
            {
                IReadOnlyList<IDomainEvent> domainEvents = entity.DomainEvents;
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();
        
        return domainEvents;
    }
}