using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEService.Core.ApplicationServices.Commands
{
    public abstract class CommandDispatcherDecorator : ICommandDispatcher
    {
        protected ICommandDispatcher _commandDispatcher;
        public CommandDispatcherDecorator(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        public abstract Task<CommandResult> Send<TCommand>(in TCommand command) where TCommand : class, ICommand;

        public abstract Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command) where TCommand : class, ICommand<TData>;
    }
}
