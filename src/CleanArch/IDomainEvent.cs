namespace CleanArch;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
