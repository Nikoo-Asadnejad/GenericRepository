using GenericRepository.Domain.Enums;

namespace GenericRepository.Domain.ValueObjects;

public class OutBoxMessageEventType : ValueObject
{
    public OutBoxMessageEventType(EventTypeEnum eventType)
    {
        EventType = eventType;
    }
    public EventTypeEnum EventType { get; init; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return EventType;
    }
}

