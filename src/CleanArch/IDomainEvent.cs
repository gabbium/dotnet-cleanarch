namespace CleanArch;

public interface IDomainEvent
{
    DateTimeOffset RaisedAt { get; }
}
