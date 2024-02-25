namespace GenericRepository.Entities;

public class ChangeHistoryEntity : BaseEntity
{
    public long Id { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public string RelatedEntityId { get; set; }
    public string RelatedEntityType { get; set; }
}