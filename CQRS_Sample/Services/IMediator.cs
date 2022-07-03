using CQRS_Sample.Commands;
using CQRS_Sample.Data.CommandHandlers;
using CQRS_Sample.Queries;

namespace CQRS_Sample.Services
{
    public interface IMediator
    {
        CommandHandlerResult Run(ICommand command);
        TResult Run<TResult>(IQuery<TResult> query);
        Task<CommandHandlerResult> RunAsync(ICommand command, CancellationToken token);
        Task<TResult> RunAsync<TResult>(IQuery<TResult> query, CancellationToken token);
    }
}