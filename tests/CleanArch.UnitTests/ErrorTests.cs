namespace CleanArch.UnitTests;

public class ErrorTests
{
    [Fact]
    public void Validation_CreatesValidationError()
    {
        // Act
        var error = Error.Validation("Validation");

        // Assert
        error.Description.ShouldBe("Validation");
        error.Type.ShouldBe(ErrorType.Validation);
    }

    [Fact]
    public void NotFound_CreatesNotFoundError()
    {
        // Act
        var error = Error.NotFound("Not found");

        // Assert
        error.Description.ShouldBe("Not found");
        error.Type.ShouldBe(ErrorType.NotFound);
    }

    [Fact]
    public void Conflict_CreatesConflictError()
    {
        // Act
        var error = Error.Conflict("Conflict");

        // Assert
        error.Description.ShouldBe("Conflict");
        error.Type.ShouldBe(ErrorType.Conflict);
    }

    [Fact]
    public void Unauthorized_CreatesUnauthorizedError()
    {
        // Act
        var error = Error.Unauthorized("Unauthorized");

        // Assert
        error.Description.ShouldBe("Unauthorized");
        error.Type.ShouldBe(ErrorType.Unauthorized);
    }

    [Fact]
    public void Forbidden_CreatesForbiddenError()
    {
        // Act
        var error = Error.Forbidden("Forbidden");

        // Assert
        error.Description.ShouldBe("Forbidden");
        error.Type.ShouldBe(ErrorType.Forbidden);
    }

    [Fact]
    public void Business_CreatesBusinessError()
    {
        // Act
        var error = Error.Business("Business");

        // Assert
        error.Description.ShouldBe("Business");
        error.Type.ShouldBe(ErrorType.Business);
    }

    [Fact]
    public void Failure_CreatesFailureError()
    {
        // Act
        var error = Error.Failure("Failure");

        // Assert
        error.Description.ShouldBe("Failure");
        error.Type.ShouldBe(ErrorType.Failure);
    }
}
