using GenericRepository.Domain.Enums;

namespace GenericRepository.Domain.ValueObjects;

public class OutBoxMessageEventType : ValueObject
{
    public EventTypeEnum EventType { get; init; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}

