namespace CleanArch;

public interface IDomainEvent
{
    DateTimeOffset Timestamp { get; }
}
