namespace CleanArch;

public interface IDomainEvent
{
    DateTime Timestamp { get; }
}
