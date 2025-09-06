namespace CleanArch.UnitTests;

public class DomainExceptionTests
{
    [Fact]
    public void Ctor_CreatesDomainException()
    {
        // Arrange
        var error = Error.Validation("Test error");

        // Act
        var exception = new DomainException(error);

        // Assert
        Assert.Equal(error, exception.Error);
    }
}
