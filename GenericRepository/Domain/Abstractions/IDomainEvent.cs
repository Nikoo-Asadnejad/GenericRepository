using GenericRepository.Domain.Enums;
using MediatR;

namespace GenericRepository.Domain;

public interface IDomainEvent : INotification
{
    public EventTypeEnum EventType { get; protected set; }
}