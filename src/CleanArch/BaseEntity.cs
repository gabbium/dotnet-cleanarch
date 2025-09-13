namespace CleanArch;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    private readonly List<DomainEventBase> _domainEvents = [];

    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents;

    public void AddDomainEvent(DomainEventBase domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
