namespace CleanArch.UnitTests;

public class ValidationErrorTests
{
    [Fact]
    public void Ctor_CreatesValidationErrorList()
    {
        // Arrange
        var propName = "Prop1";
        var message = "First Error";

        // Act
        var error = new ValidationError(propName, message);

        // Assert
        Assert.Equal(propName, error.PropertyName);
        Assert.Equal(message, error.Message);
    }
}
