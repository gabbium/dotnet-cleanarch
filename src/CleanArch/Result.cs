namespace CleanArch;

public class Result<T>(T? value, bool isSuccess, Error? error) : Result(isSuccess, error)
{
    public T? Value => value;

    public static Result<T> Success(T value)
    {
        return new(value, true, null);
    }

    public static new Result<T> Failure(Error error)
    {
        return new(default, false, error);
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return Failure(error);
    }
}
