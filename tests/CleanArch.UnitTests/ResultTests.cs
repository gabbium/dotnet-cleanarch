namespace CleanArch.UnitTests;

public class ResultTests
{
    [Fact]
    public void Ctor_WhenInvalidErrorCombination_ThenThrowsArgumentException()
    {
        // Assert
        Assert.Throws<InvalidOperationException>(() => new Result(true, Error.Failure("Y")));
        Assert.Throws<InvalidOperationException>(() => new Result(false, null));
    }

    [Fact]
    public void Success_CreatesSuccessResult()
    {
        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Success_CreatesSuccessResultWithValue()
    {
        // Act
        var result = Result<string>.Success("hello");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("hello", result.Value);
    }

    [Fact]
    public void Failure_CreatesFailureResult()
    {
        // Arrange
        var error = Error.Failure("Something went wrong");

        // Act
        var result = Result.Failure(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Failure_CreatesFailureResultWithError()
    {
        var error = Error.Failure("Failure");

        // Act
        var result = Result<string>.Failure(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
        Assert.Throws<InvalidOperationException>(() => _ = result.Value);
    }

    [Fact]
    public void ImplicitConversion_WhenValue_ThenCreatesSuccessResult()
    {
        // Act
        Result<string> result = "test";

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("test", result.Value);
    }

    [Fact]
    public void ImplicitConversion_WhenError_ThenCreatesFailure()
    {
        // Act
        Result result = Error.Failure("Failure");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
    }

    [Fact]
    public void ImplicitConversion_WhenValueButError_ThenCreatesFailure()
    {
        // Act
        Result<string> result = Error.Failure("Failure");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
    }
}
