using System.Text.Json;
using GenericRepository.Domain.Enums;
using GenericRepository.Domain.ValueObjects;

namespace GenericRepository.Domain.Entities;

public class OutBoxMessage : Entity
{
    private OutBoxMessage(string type, string content,
        EventTypeEnum eventType = EventTypeEnum.Internal)
    {
        EntityType = type;
        Content = content;
        OccurredOnUtc = DateTime.UtcNow;
        EventType = new OutBoxMessageEventType(eventType);
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
    public OutBoxMessageEventType EventType { get; init; }
    public bool IsProcessed => ProcessedOnUtc.HasValue;
    public void Process(string? error = null)
    {
        ProcessedOnUtc = DateTime.UtcNow;
        Error = error;
    }
}