namespace CleanArch.UnitTests;

public class ValidationBehaviorCommandHandlerTests
{
    public record TestCommand(string Value) : ICommand<string>;

    [Fact]
    public async Task HandleAsync_WhenNoValidators_ThenCallsInnerHandler()
    {
        // Arrange
        var mockInnerHandler = new Mock<ICommandHandler<TestCommand, string>>();
        mockInnerHandler
            .Setup(h => h.HandleAsync(It.IsAny<TestCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success("OK"));

        var handler = new ValidationBehavior.CommandHandler<TestCommand, string>(
            mockInnerHandler.Object,
            []
        );

        var command = new TestCommand("data");

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe("OK");
        mockInnerHandler.Verify(h => h.HandleAsync(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenValidationPasses_ThenCallsInnerHandler()
    {
        // Arrange
        var mockValidator = new Mock<IValidator<TestCommand>>();
        mockValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestCommand>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var mockInnerHandler = new Mock<ICommandHandler<TestCommand, string>>();
        mockInnerHandler
            .Setup(h => h.HandleAsync(It.IsAny<TestCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success("OK"));

        var handler = new ValidationBehavior.CommandHandler<TestCommand, string>(
            mockInnerHandler.Object,
            [mockValidator.Object]
        );

        var command = new TestCommand("data");

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe("OK");
        mockInnerHandler.Verify(h => h.HandleAsync(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenValidationFails_ThenReturnsFailureAndSkipInnerHandler()
    {
        // Arrange
        var failures = new[]
        {
            new ValidationFailure("Field1", "Error 1") { ErrorCode = "Code1" },
            new ValidationFailure("Field2", "Error 2") { ErrorCode = "Code2" }
        };

        var mockValidator = new Mock<IValidator<TestCommand>>();
        mockValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestCommand>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(failures));

        var mockInnerHandler = new Mock<ICommandHandler<TestCommand, string>>();

        var handler = new ValidationBehavior.CommandHandler<TestCommand, string>(
            mockInnerHandler.Object,
            [mockValidator.Object]
        );

        var command = new TestCommand("invalid");

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        var errorList = result.Error.ShouldBeOfType<ValidationErrorList>();
        errorList.Type.ShouldBe(ErrorType.Validation);
        errorList.Errors.Count.ShouldBe(2);

        mockInnerHandler.Verify(h => h.HandleAsync(It.IsAny<TestCommand>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}

public class ValidationBehaviorCommandBaseHandlerTests
{
    public record TestCommand() : ICommand;

    [Fact]
    public async Task HandleAsync_WhenNoValidators_ThenCallsInnerHandler()
    {
        // Arrange
        var mockInnerHandler = new Mock<ICommandHandler<TestCommand>>();
        mockInnerHandler
            .Setup(h => h.HandleAsync(It.IsAny<TestCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        var handler = new ValidationBehavior.CommandHandler<TestCommand>(
            mockInnerHandler.Object,
            []
        );

        var command = new TestCommand();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        mockInnerHandler.Verify(h => h.HandleAsync(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenValidationFails_ThenReturnsFailureAndSkipInnerHandler()
    {
        // Arrange
        var failures = new[]
        {
            new ValidationFailure("Field", "Base Error") { ErrorCode = "BaseCode" }
        };

        var mockValidator = new Mock<IValidator<TestCommand>>();
        mockValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestCommand>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(failures));

        var mockInnerHandler = new Mock<ICommandHandler<TestCommand>>();

        var handler = new ValidationBehavior.CommandHandler<TestCommand>(
            mockInnerHandler.Object,
            [mockValidator.Object]
        );

        var command = new TestCommand();

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        var errorList = result.Error.ShouldBeOfType<ValidationErrorList>();
        errorList.Errors.Count.ShouldBe(1);

        mockInnerHandler.Verify(h => h.HandleAsync(It.IsAny<TestCommand>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}

public class ValidationBehaviorQueryHandlerTests
{
    public record TestQuery(string Query) : IQuery<int>;

    [Fact]
    public async Task HandleAsync_WhenValidationPasses_ThenCallsInnerHandler()
    {
        // Arrange
        var mockValidator = new Mock<IValidator<TestQuery>>();
        mockValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestQuery>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var mockInnerHandler = new Mock<IQueryHandler<TestQuery, int>>();
        mockInnerHandler
            .Setup(h => h.HandleAsync(It.IsAny<TestQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<int>.Success(42));

        var handler = new ValidationBehavior.QueryHandler<TestQuery, int>(
            mockInnerHandler.Object,
            [mockValidator.Object]
        );

        var query = new TestQuery("ok");

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(42);
        mockInnerHandler.Verify(h => h.HandleAsync(query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenValidationFails_ThenReturnsFailureAndSkipInnerHandler()
    {
        // Arrange
        var failures = new[]
        {
            new ValidationFailure("QueryField", "Invalid query") { ErrorCode = "QErr" }
        };

        var mockValidator = new Mock<IValidator<TestQuery>>();
        mockValidator
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestQuery>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(failures));

        var mockInnerHandler = new Mock<IQueryHandler<TestQuery, int>>();

        var handler = new ValidationBehavior.QueryHandler<TestQuery, int>(
            mockInnerHandler.Object,
            [mockValidator.Object]
        );

        var query = new TestQuery("bad");

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        var errorList = result.Error.ShouldBeOfType<ValidationErrorList>();
        errorList.Errors.Count.ShouldBe(1);

        mockInnerHandler.Verify(h => h.HandleAsync(It.IsAny<TestQuery>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
