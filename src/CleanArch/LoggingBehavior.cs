namespace CleanArch;

public static class LoggingBehavior
{
    public sealed class CommandHandler<TCommand>(
        ICommandHandler<TCommand> inner,
        ILogger<CommandHandler<TCommand>> logger)
        : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        {
            var name = typeof(TCommand).Name;

            logger.LogInformation("Handling command {CommandName} {@Command}", name, command);

            var result = await inner.HandleAsync(command, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation("Handled command {CommandName}", name);
            }
            else
            {
                logger.LogError("Command {CommandName} failed with error {@Error}", name, result.Error);
            }

            return result;
        }
    }

    public sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> inner,
        ILogger<CommandHandler<TCommand, TResponse>> logger)
        : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        {
            var name = typeof(TCommand).Name;

            logger.LogInformation("Handling command {CommandName} {@Command}", name, command);

            var result = await inner.HandleAsync(command, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation("Handled command {CommandName} with response {@Response}", name, result.Value);
            }
            else
            {
                logger.LogError("Command {CommandName} failed with error {@Error}", name, result.Error);
            }

            return result;
        }
    }

    public sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> inner,
        ILogger<QueryHandler<TQuery, TResponse>> logger)
        : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            var name = typeof(TQuery).Name;

            logger.LogInformation("Handling query {QueryName} {@Query}", name, query);

            var result = await inner.HandleAsync(query, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation("Handled query {QueryName} with response {@Response}", name, result.Value);
            }
            else
            {
                logger.LogError("Query {QueryName} failed with error {@Error}", name, result.Error);
            }

            return result;
        }
    }
}
