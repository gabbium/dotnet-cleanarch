namespace CleanArch.UnitTests;

public class ResultTests
{
    [Fact]
    public void Ctor_WhenInvalidErrorCombination_ThenThrowsArgumentException()
    {
        // Assert
        Should.Throw<InvalidOperationException>(() => new Result(true, Error.Failure("Y")));
        Should.Throw<InvalidOperationException>(() => new Result(false, null));
    }

    [Fact]
    public void Success_CreatesSuccessResult()
    {
        // Act
        var result = Result.Success();

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
        result.Error.ShouldBeNull();
    }

    [Fact]
    public void Success_CreatesSuccessResultWithValue()
    {
        // Act
        var result = Result<string>.Success("hello");

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe("hello");
    }

    [Fact]
    public void Failure_CreatesFailureResult()
    {
        // Arrange
        var error = Error.Failure("Something went wrong");

        // Act
        var result = Result.Failure(error);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(error);
    }

    [Fact]
    public void Failure_CreatesFailureResultWithError()
    {
        // Arrange
        var error = Error.Failure("Failure");

        // Act
        var result = Result<string>.Failure(error);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(error);
        result.Value.ShouldBeNull();
    }

    [Fact]
    public void ImplicitConversion_WhenValue_ThenCreatesSuccessResult()
    {
        // Act
        Result<string> result = "test";

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe("test");
    }

    [Fact]
    public void ImplicitConversion_WhenError_ThenCreatesFailure()
    {
        // Act
        Result result = Error.Failure("Failure");

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
    }

    [Fact]
    public void ImplicitConversion_WhenValueButError_ThenCreatesFailure()
    {
        // Act
        Result<string> result = Error.Failure("Failure");

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
    }
}
