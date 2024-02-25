using GenericRepository.Abstractions;

namespace GenericRepository.Entities;

public abstract class BaseEntity : IEquatable<BaseEntity>
{
    public long Id { get; private set; }
    public long CreateDate { get; private set; }
    public long? UpdateDate { get; private set; }
    public long? DeleteDate { get; private set; }
    public bool IsDeleted { get; private set; } = false;

    private int? _hashCode;
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private List<IDomainEvent> _domainEvents = new();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public BaseEntity Create()
    {
        this.CreateDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        return this;
    }

    public BaseEntity Update()
    {
        if (CreateDate == default)
            throw new Exception("Entity State is not valid for updating");

        this.UpdateDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        return this;
    }

    public BaseEntity Delete()
    {
        if (CreateDate == default)
            throw new Exception("Entity State is not valid for deleting");

        this.DeleteDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        this.IsDeleted = true;
        return this;
    }

    public bool IsTransient()
    {
        return this.Id == default;
    }

    public bool Equals(BaseEntity? other)
    {
        if (other is null)
            return false;

        if (this.GetType() != other.GetType())
            return false;

        if (Object.ReferenceEquals(this, other))
            return true;

        BaseEntity item = other;

        if (item.IsTransient() || this.IsTransient())
            return false;

        return item.Id == this.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BaseEntity)
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        BaseEntity item = (BaseEntity)obj;

        if (item.IsTransient() || this.IsTransient())
            return false;
        else
            return item.Id == this.Id;
    }

    public override int GetHashCode()
    {
        if (IsTransient())
            return base.GetHashCode();

        if (!_hashCode.HasValue)
            _hashCode = this.Id.GetHashCode() ^ 31;
        
        return _hashCode.Value;
    }

    public static bool operator ==(BaseEntity left, BaseEntity right)
    {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null));

        return left.Equals(right);
    }

    public static bool operator !=(BaseEntity left, BaseEntity right)
    {
        return !(left == right);
    }
}