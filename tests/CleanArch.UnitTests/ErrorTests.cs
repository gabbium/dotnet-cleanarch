namespace CleanArch.UnitTests;

public class ErrorTests
{
    [Fact]
    public void Validation_CreatesValidationError()
    {
        // Act
        var error = Error.Validation("A validation error occurred");

        // Assert
        Assert.Equal("A validation error occurred", error.Description);
        Assert.Equal(ErrorType.Validation, error.Type);
    }

    [Fact]
    public void NotFound_CreatesNotFoundError()
    {
        // Act
        var error = Error.NotFound("Not found");

        // Assert
        Assert.Equal("Not found", error.Description);
        Assert.Equal(ErrorType.NotFound, error.Type);
    }

    [Fact]
    public void Conflict_CreatesConflictError()
    {
        // Act
        var error = Error.Conflict("Conflict");

        // Assert
        Assert.Equal("Conflict", error.Description);
        Assert.Equal(ErrorType.Conflict, error.Type);
    }

    [Fact]
    public void Unauthorized_CreatesUnauthorizedError()
    {
        // Act
        var error = Error.Unauthorized("Unauthorized");

        // Assert
        Assert.Equal("Unauthorized", error.Description);
        Assert.Equal(ErrorType.Unauthorized, error.Type);
    }

    [Fact]
    public void Forbidden_CreatesForbiddenError()
    {
        // Act
        var error = Error.Forbidden("Forbidden");

        // Assert
        Assert.Equal("Forbidden", error.Description);
        Assert.Equal(ErrorType.Forbidden, error.Type);
    }

    [Fact]
    public void Failure_CreatesFailureError()
    {
        // Act
        var error = Error.Failure("Test failure");

        // Assert
        Assert.Equal("Test failure", error.Description);
        Assert.Equal(ErrorType.Failure, error.Type);
    }
}
