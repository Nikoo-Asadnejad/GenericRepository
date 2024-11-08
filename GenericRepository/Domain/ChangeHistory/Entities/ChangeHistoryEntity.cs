using System.Text.Json;
using System.Text.Json.Serialization;

namespace GenericRepository.Domain.Entities;

public class ChangeHistoryEntity : Entity
{
    public string OldValue { get; private set; }
    
    public string NewValue { get;private set; }
    
    public string RelatedEntityId { get; private set; }
    
    public string RelatedEntityType { get; private set; }

    public void LogHistory(Entity oldEntity , Entity newEntity)
    {
        OldValue = JsonSerializer.Serialize(oldEntity);
        NewValue = JsonSerializer.Serialize(oldEntity);
        RelatedEntityId = oldEntity.Id.ToString();
        RelatedEntityType = oldEntity.GetType().Name;
    }
}