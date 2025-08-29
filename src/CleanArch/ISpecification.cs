namespace CleanArch;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}
