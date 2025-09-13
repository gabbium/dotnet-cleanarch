namespace CleanArch;

public static class ValidationBehavior
{
    public sealed class CommandHandler<TCommand>(
        ICommandHandler<TCommand> inner,
        IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly IValidator<TCommand>[] _validators = [.. validators];

        public async Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        {
            var validationFailures = await ValidateAsync(command, _validators, cancellationToken);

            if (validationFailures.Errors.Count == 0)
            {
                return await inner.HandleAsync(command, cancellationToken);
            }

            return validationFailures;
        }
    }

    public sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> inner,
        IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        private readonly IValidator<TCommand>[] _validators = [.. validators];

        public async Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        {
            var validationFailures = await ValidateAsync(command, _validators, cancellationToken);

            if (validationFailures.Errors.Count == 0)
            {
                return await inner.HandleAsync(command, cancellationToken);
            }

            return validationFailures;
        }
    }

    public sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> inner,
        IEnumerable<IValidator<TQuery>> validators)
        : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        private readonly IValidator<TQuery>[] _validators = [.. validators];

        public async Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            var validationFailures = await ValidateAsync(query, _validators, cancellationToken);

            if (validationFailures.Errors.Count == 0)
            {
                return await inner.HandleAsync(query, cancellationToken);
            }

            return validationFailures;
        }
    }

    private static async Task<ValidationErrorList> ValidateAsync<T>(
        T message,
        IValidator<T>[] validators,
        CancellationToken cancellationToken)
    {
        if (validators.Length == 0)
        {
            return new ValidationErrorList(Array.Empty<ValidationError>());
        }

        var context = new ValidationContext<T>(message);

        var failures = new List<ValidationError>(8);

        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(context, cancellationToken);

            foreach (var error in result.Errors)
            {
                failures.Add(new ValidationError(error.PropertyName, error.ErrorMessage));
            }
        }

        return failures.Count == 0
            ? new ValidationErrorList(Array.Empty<ValidationError>())
            : new ValidationErrorList(failures);
    }
}
