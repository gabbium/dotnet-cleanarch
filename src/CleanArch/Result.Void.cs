namespace CleanArch;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    public Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error is not null || !isSuccess && error is null)
        {
            throw new InvalidOperationException("Invalid result state.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
    {
        return new(true, null);
    }

    public static Result Failure(Error error)
    {
        return new(false, error);
    }

    public static implicit operator Result(Error error)
    {
        return Failure(error);
    }
}
