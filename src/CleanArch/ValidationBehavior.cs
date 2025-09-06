namespace CleanArch;

public static class ValidationBehavior
{
    public sealed class CommandHandler<TCommand>(
        ICommandHandler<TCommand> inner,
        IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> HandleAsync(
            TCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationFailures = await ValidateAsync(command, validators, cancellationToken);

            if (validationFailures.Length == 0)
                return await inner.HandleAsync(command, cancellationToken);

            return CreateValidationError(validationFailures);
        }
    }

    public sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> inner,
        IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(
            TCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationFailures = await ValidateAsync(command, validators, cancellationToken);

            if (validationFailures.Length == 0)
                return await inner.HandleAsync(command, cancellationToken);

            return CreateValidationError(validationFailures);
        }
    }

    public sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> inner,
        IEnumerable<IValidator<TQuery>> validators)
        : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(
            TQuery query,
            CancellationToken cancellationToken = default)
        {
            var validationFailures = await ValidateAsync(query, validators, cancellationToken);

            if (validationFailures.Length == 0)
                return await inner.HandleAsync(query, cancellationToken);

            return CreateValidationError(validationFailures);
        }
    }

    public static async Task<ValidationFailure[]> ValidateAsync<T>(
        T message,
        IEnumerable<IValidator<T>> validators,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return [];

        var context = new ValidationContext<T>(message);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var validationFailures = validationResults
            .SelectMany(r => r.Errors)
            .Where(e => e is not null)
            .ToArray();

        return validationFailures;
    }

    private static ValidationErrorList CreateValidationError(ValidationFailure[] validationFailures)
    {
        return new([.. validationFailures.Select(
            v => new ValidationError(v.PropertyName, v.ErrorMessage))]);
    }
}
