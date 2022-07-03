using CQRS_Sample.Commands;
using CQRS_Sample.Data.CommandHandlers;
using CQRS_Sample.Data.QueryHandlers;
using CQRS_Sample.Queries;

namespace CQRS_Sample.Services
{
    public sealed class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResult Run<TResult>(IQuery<TResult> query)
        {
            Type type = typeof(IQueryHandler<,>);
            Type[] typeArgs = { query.GetType(), typeof(TResult) };
            Type handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = _serviceProvider.GetService(handlerType);
            TResult result = handler.Handle((dynamic)query);
            return result;
        }

        public async Task<TResult> RunAsync<TResult>(IQuery<TResult> query, CancellationToken token)
        {
            Type type = typeof(IAsyncQueryHandler<,>);
            Type[] typeArgs = { query.GetType(), typeof(TResult) };
            Type handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = _serviceProvider.GetService(handlerType);
            TResult result = await handler.Handle((dynamic)query, token);
            return result;
        }

        public CommandHandlerResult Run(ICommand command)
        {
            Type type = typeof(ICommandHandler<>);
            Type[] typeArgs = { command.GetType() };
            Type handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = _serviceProvider.GetService(handlerType);
            CommandHandlerResult result = handler.Handle((dynamic)command);
            return result;
        }

        public async Task<CommandHandlerResult> RunAsync(ICommand command, CancellationToken token)
        {
            Type type = typeof(IAsyncCommandHandler<>);
            Type[] typeArgs = { command.GetType() };
            Type handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = _serviceProvider.GetService(handlerType);
            CommandHandlerResult result = await handler.Handle((dynamic)command, token);
            return result;
        }
    }
}
