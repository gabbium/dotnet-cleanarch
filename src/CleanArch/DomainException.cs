namespace CleanArch;

public class DomainException(Error error) : Exception(error.Description)
{
    public Error Error { get; } = error;
}
