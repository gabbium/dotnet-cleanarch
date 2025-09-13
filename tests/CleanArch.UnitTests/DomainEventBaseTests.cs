namespace CleanArch.UnitTests;

public class DomainEventBaseTests
{
    public class TestDomainEvent : DomainEventBase;

    [Fact]
    public void Ctor_CreatesDomainEvent()
    {
        // Act
        var domainEvent = new TestDomainEvent();

        // Assert
        domainEvent.RaisedAt.ShouldNotBe(DateTime.MinValue);
    }
}
