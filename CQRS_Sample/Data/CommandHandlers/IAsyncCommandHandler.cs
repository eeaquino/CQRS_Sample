using CQRS_Sample.Commands;

namespace CQRS_Sample.Data.CommandHandlers
{
    public interface IAsyncCommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<CommandHandlerResult> Handle(TCommand command, CancellationToken ct);
    }
}
