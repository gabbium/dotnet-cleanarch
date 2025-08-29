namespace CleanArch.UnitTests;

public class ErrorTests
{
    [Fact]
    public void Failure_CreatesFailureError()
    {
        // Act
        var error = Error.Failure("TestCode", "Test failure");

        // Assert
        Assert.Equal("TestCode", error.Code);
        Assert.Equal("Test failure", error.Description);
        Assert.Equal(ErrorType.Failure, error.Type);
    }

    [Fact]
    public void Validation_CreatesValidationError()
    {
        // Act
        var error = Error.Validation("ValidationCode", "A validation error occurred");

        // Assert
        Assert.Equal("ValidationCode", error.Code);
        Assert.Equal("A validation error occurred", error.Description);
        Assert.Equal(ErrorType.Validation, error.Type);
    }

    [Fact]
    public void Problem_CreatesProblemError()
    {
        // Act
        var error = Error.Problem("ProblemCode", "A problem occurred");

        // Assert
        Assert.Equal("ProblemCode", error.Code);
        Assert.Equal("A problem occurred", error.Description);
        Assert.Equal(ErrorType.Problem, error.Type);
    }

    [Fact]
    public void NotFound_CreatesNotFoundError()
    {
        // Act
        var error = Error.NotFound("NotFoundCode", "Not found");

        // Assert
        Assert.Equal("NotFoundCode", error.Code);
        Assert.Equal("Not found", error.Description);
        Assert.Equal(ErrorType.NotFound, error.Type);
    }

    [Fact]
    public void Conflict_CreatesConflictError()
    {
        // Act
        var error = Error.Conflict("ConflictCode", "Conflict found");

        // Assert
        Assert.Equal("ConflictCode", error.Code);
        Assert.Equal("Conflict found", error.Description);
        Assert.Equal(ErrorType.Conflict, error.Type);
    }

    [Fact]
    public void Instiate_None()
    {
        // Act
        var error = Error.None;

        // Assert
        Assert.Equal(string.Empty, error.Code);
        Assert.Equal(string.Empty, error.Description);
        Assert.Equal(ErrorType.Failure, error.Type);
    }

    [Fact]
    public void Instantiate_NullValue()
    {
        // Act
        var error = Error.NullValue;

        // Assert
        Assert.Equal("General.Null", error.Code);
        Assert.Equal("Null value was provided", error.Description);
        Assert.Equal(ErrorType.Failure, error.Type);
    }
}
