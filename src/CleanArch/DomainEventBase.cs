namespace CleanArch;

public abstract class DomainEventBase : IDomainEvent
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}
