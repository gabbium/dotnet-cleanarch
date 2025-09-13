namespace CleanArch.UnitTests;

public class MediatorTests
{
    public record Ping() : ICommand;
    public record Add(int A, int B) : ICommand<int>;
    public record Echo(string Text) : IQuery<string>;

    [Fact]
    public async Task SendAsync_WhenCommandWithoutResponse_ThenInvokesHandlerAndReturnsSuccess()
    {
        // Arrange
        var services = new ServiceCollection();

        var pingHandler = new Mock<ICommandHandler<Ping>>();
        pingHandler
            .Setup(h => h.HandleAsync(It.IsAny<Ping>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success())
            .Verifiable();

        services.AddSingleton(pingHandler.Object);

        var sp = services.BuildServiceProvider();
        var mediator = new Mediator(sp);

        var msg = new Ping();

        // Act
        var res = await mediator.SendAsync(msg, CancellationToken.None);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        pingHandler.Verify(h => h.HandleAsync(msg, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendAsync_WhenCommandWithResponse_ThenInvokesHandlerAndReturnsValue()
    {
        // Arrange
        var services = new ServiceCollection();

        var addHandler = new Mock<ICommandHandler<Add, int>>();
        addHandler
            .Setup(h => h.HandleAsync(It.IsAny<Add>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<int>.Success(5))
            .Verifiable();

        services.AddSingleton(addHandler.Object);

        var sp = services.BuildServiceProvider();
        var mediator = new Mediator(sp);

        var msg = new Add(2, 3);

        // Act
        var res = await mediator.SendAsync(msg, CancellationToken.None);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.ShouldBe(5);
        addHandler.Verify(h => h.HandleAsync(msg, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendAsync_WhenQueryWithResponse_ThenInvokesHandlerAndReturnsValue()
    {
        // Arrange
        var services = new ServiceCollection();

        var echoHandler = new Mock<IQueryHandler<Echo, string>>();
        echoHandler
            .Setup(h => h.HandleAsync(It.IsAny<Echo>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success("hi"))
            .Verifiable();

        services.AddSingleton(echoHandler.Object);

        var sp = services.BuildServiceProvider();
        var mediator = new Mediator(sp);

        var query = new Echo("hi");

        // Act
        var res = await mediator.SendAsync(query, CancellationToken.None);

        // Assert
        res.IsSuccess.ShouldBeTrue();
        res.Value.ShouldBe("hi");
        echoHandler.Verify(h => h.HandleAsync(query, It.IsAny<CancellationToken>()), Times.Once);
    }
}
