namespace GenericRepository.Domain.Entities;

public class ChangeHistoryEntity : BaseEntity
{

    public string OldValue { get; private set; }
    
    public string NewValue { get;private set; }
    
    public string RelatedEntityId { get; private set; }
    public string RelatedEntityType { get; private set; }
}