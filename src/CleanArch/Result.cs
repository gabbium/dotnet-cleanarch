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

    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);

    public static implicit operator Result(Error error) => Failure(error);
}

public class Result<T>(T? value, bool isSuccess, Error? error) : Result(isSuccess, error)
{
    public T Value => IsSuccess
        ? value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed");

    public static Result<T> Success(T value) => new(value, true, null);
    public static new Result<T> Failure(Error error) => new(default, false, error);

    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Error error) => Failure(error);
}
