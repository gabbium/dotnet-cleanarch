namespace CleanArch.UnitTests;

public class SenderTests
{
    public record Ping() : ICommand;
    public record Add(int A, int B) : ICommand<int>;
    public record Echo(string Text) : IQuery<string>;

    [Fact]
    public async Task Send_WhenCommandWithoutResponse_ThenInvokesHandlerAndReturnsSuccess()
    {
        // Arrange
        var services = new ServiceCollection();

        var pingHandler = new Mock<ICommandHandler<Ping>>();
        pingHandler.Setup(h => h.HandleAsync(It.IsAny<Ping>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(Result.Success())
                   .Verifiable();

        services.AddSingleton(pingHandler.Object);

        var sp = services.BuildServiceProvider();
        var sender = new Sender(sp);

        var msg = new Ping();

        // Act
        var res = await sender.Send(msg, CancellationToken.None);

        // Assert
        Assert.True(res.IsSuccess);
        pingHandler.Verify(h => h.HandleAsync(msg, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Send_WhenCommandWithResponse_ThenInvokesHandlerAndReturnsValue()
    {
        // Arrange
        var services = new ServiceCollection();

        var addHandler = new Mock<ICommandHandler<Add, int>>();
        addHandler.Setup(h => h.HandleAsync(It.IsAny<Add>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(Result.Success(5))
                  .Verifiable();

        services.AddSingleton(addHandler.Object);

        var sp = services.BuildServiceProvider();
        var sender = new Sender(sp);

        var msg = new Add(2, 3);

        // Act
        var res = await sender.Send(msg, CancellationToken.None);

        // Assert
        Assert.True(res.IsSuccess);
        Assert.Equal(5, res.Value);
        addHandler.Verify(h => h.HandleAsync(msg, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Send_WhenQueryWithResponse_ThenInvokesHandlerAndReturnsValue()
    {
        // Arrange
        var services = new ServiceCollection();

        var echoHandler = new Mock<IQueryHandler<Echo, string>>();
        echoHandler.Setup(h => h.HandleAsync(It.IsAny<Echo>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(Result.Success("hi"))
                   .Verifiable();

        services.AddSingleton(echoHandler.Object);

        var sp = services.BuildServiceProvider();
        var sender = new Sender(sp);

        var query = new Echo("hi");

        // Act
        var res = await sender.Send(query, CancellationToken.None);

        // Assert
        Assert.True(res.IsSuccess);
        Assert.Equal("hi", res.Value);
        echoHandler.Verify(h => h.HandleAsync(query, It.IsAny<CancellationToken>()), Times.Once);
    }
}
