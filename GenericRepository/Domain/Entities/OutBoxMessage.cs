using System.Text.Json;

namespace GenericRepository.Domain.Entities;

public class OutBoxMessage : BaseEntity
{
    private OutBoxMessage(string type , string content)
    {
        Type = type;
        Content = content;
        OccurredOnUtc = DateTime.UtcNow;
    }
    
    public OutBoxMessage CreateByDomainEvent(IDomainEvent domainEvent) 
    {
       return new OutBoxMessage(domainEvent.GetType().Name, JsonSerializer.Serialize(domainEvent));
    }

    public int Id { get; init; }
    public string Type { get; init; }
    public string Content { get; init; }
    public DateTime OccurredOnUtc { get; init; }
    public DateTime? ProcessedOnUtc { get; private set; }
    public string? Error { get; private set; }

    public void Process(string? error = null)
    {
        ProcessedOnUtc = DateTime.UtcNow;
        Error = error;
    }
}