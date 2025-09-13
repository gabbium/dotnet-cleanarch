namespace CleanArch.UnitTests;

public class BaseEntityTests
{
    public class TestEntity : BaseEntity;

    public class TestDomainEvent : DomainEventBase;

    [Fact]
    public void Ctor_CreatesEntity()
    {
        // Act
        var entity = new TestEntity();

        // Assert
        entity.Id.ShouldNotBe(Guid.Empty);
        entity.DomainEvents.ShouldBeEmpty();
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
        entity.DomainEvents.Count.ShouldBe(1);
        entity.DomainEvents.ShouldContain(domainEvent);
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
        entity.DomainEvents.ShouldBeEmpty();
    }
}
