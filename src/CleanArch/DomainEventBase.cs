namespace CleanArch;

public abstract class DomainEventBase : IDomainEvent
{
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
}
