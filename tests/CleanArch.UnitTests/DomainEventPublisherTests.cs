namespace CleanArch.UnitTests;

public class DomainEventPublisherTests
{
    public class TestDomainEvent : DomainEventBase;

    [Fact]
    public async Task PublishAsync_DispatchesToAllMatchingHandlers()
    {
        // Arrange
        var services = new ServiceCollection();

        var h1 = new Mock<IDomainEventHandler<TestDomainEvent>>();
        var h2 = new Mock<IDomainEventHandler<TestDomainEvent>>();

        h1.Setup(x => x.HandleAsync(It.IsAny<TestDomainEvent>(), It.IsAny<CancellationToken>()))
          .Returns(Task.CompletedTask)
          .Verifiable();

        h2.Setup(x => x.HandleAsync(It.IsAny<TestDomainEvent>(), It.IsAny<CancellationToken>()))
          .Returns(Task.CompletedTask)
          .Verifiable();

        services.AddSingleton(h1.Object);
        services.AddSingleton(h2.Object);

        var sp = services.BuildServiceProvider();
        var publisher = new DomainEventPublisher(sp);
        var evt = new TestDomainEvent();

        // Act
        await publisher.PublishAsync(evt, CancellationToken.None);

        // Assert
        h1.Verify(x => x.HandleAsync(evt, It.IsAny<CancellationToken>()), Times.Once);
        h2.Verify(x => x.HandleAsync(evt, It.IsAny<CancellationToken>()), Times.Once);
    }
}
