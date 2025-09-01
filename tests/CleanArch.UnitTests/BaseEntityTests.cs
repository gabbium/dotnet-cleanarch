namespace CleanArch.UnitTests;

public class BaseEntityTests
{
    public class TestEntity : BaseEntity;

    public class TestDomainEvent: DomainEventBase;

    [Fact]
    public void Ctor_CreatesEntity()
    {
        // Act
        var entity = new TestEntity();

        // Assert
        Assert.Equal(Guid.Empty, entity.Id);
        Assert.Empty(entity.DomainEvents);
    }

    [Fact]
    public void AddDomainEvent_AddsEventToDomainEvents()
    {
        // Arrange
        var entity = new TestEntity();
        var domainEvent = new TestDomainEvent();

        // Act
        entity.AddDomainEvent(domainEvent);

        // Assert
        Assert.Single(entity.DomainEvents);
        Assert.Contains(domainEvent, entity.DomainEvents);
    }

    [Fact]
    public void RemoveDomainEvent_RemovesEventFromDomainEvents()
    {
        // Arrange
        var entity = new TestEntity();
        var domainEvent = new TestDomainEvent();
        entity.AddDomainEvent(domainEvent);

        // Act
        entity.RemoveDomainEvent(domainEvent);

        // Assert
        Assert.Empty(entity.DomainEvents);
    }

    [Fact]
    public void ClearDomainEvents_ClearsAllDomainEvents()
    {
        // Arrange
        var entity = new TestEntity();
        var domainEvent1 = new TestDomainEvent();
        var domainEvent2 = new TestDomainEvent();
        entity.AddDomainEvent(domainEvent1);
        entity.AddDomainEvent(domainEvent2);

        // Act
        entity.ClearDomainEvents();

        // Assert
        Assert.Empty(entity.DomainEvents);
    }
}
