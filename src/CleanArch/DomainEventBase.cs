﻿namespace CleanArch;

public abstract class DomainEventBase : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
