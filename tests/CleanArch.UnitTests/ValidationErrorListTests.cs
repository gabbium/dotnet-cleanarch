namespace CleanArch.UnitTests;

public class ValidationErrorListTests
{
    [Fact]
    public void Ctor_CreatesValidationErrorList()
    {
        // Arrange
        var errors = new[]
        {
            new ValidationError("Prop1", "First Error"),
            new ValidationError("Prop2", "Second Error"),
        };

        // Act
        var errorList = new ValidationErrorList(errors);

        // Assert
        Assert.Equal("One or more validation errors occurred", errorList.Description);
        Assert.Equal(ErrorType.Validation, errorList.Type);
        Assert.Equal(errors, errorList.Errors);
    }
}
