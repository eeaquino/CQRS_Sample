using CQRS_Sample.Commands;

namespace CQRS_Sample.Data.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        CommandHandlerResult Handle(TCommand command);        
    }
}
