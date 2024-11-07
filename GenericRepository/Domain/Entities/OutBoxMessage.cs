using System.Text.Json;
using GenericRepository.Domain.Enums;

namespace GenericRepository.Domain.Entities;

public class OutBoxMessage : BaseEntity
{
    private OutBoxMessage(string type, string content,
        EventTypeEnum eventType = EventTypeEnum.Internal)
    {
        EntityType = type;
        Content = content;
        OccurredOnUtc = DateTime.UtcNow;
        EventType = eventType;
    }
    
    public OutBoxMessage CreateByDomainEvent(IDomainEvent domainEvent) 
    {
       return new OutBoxMessage(domainEvent.GetType().Name, JsonSerializer.Serialize(domainEvent));
    }

    public int Id { get; init; }
    public string EntityType { get; init; }
    public string Content { get; init; }
    public DateTime OccurredOnUtc { get; init; }
    public DateTime? ProcessedOnUtc { get; private set; }
    public string? Error { get; private set; }
    public EventTypeEnum EventType { get; init; }
    public void Process(string? error = null)
    {
        ProcessedOnUtc = DateTime.UtcNow;
        Error = error;
    }
}