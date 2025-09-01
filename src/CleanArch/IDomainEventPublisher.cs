namespace CleanArch;

public interface IDomainEventPublisher
{
    Task PublishAsync(IDomainEvent @event, CancellationToken cancellationToken = default);
}
