using OKEService.Core.Domain.Common;
using OKEService.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEService.Core.ApplicationServices.Commands
{
    public abstract class CommandHandler<TCommand, TData> : ICommandHandler<TCommand, TData>
   where TCommand : ICommand<TData>
    {
        protected readonly OKEServiceServices _hamoonServices;
        protected readonly CommandResult<TData> result = new CommandResult<TData>();

        public CommandHandler(OKEServiceServices hamoonServices)
        {
            _hamoonServices = hamoonServices;
        }
        public abstract Task<CommandResult<TData>> Handle(TCommand request);
        protected virtual Task<CommandResult<TData>> OkAsync(TData data)
        {
            result._data = data;
            result.Status = ApplicationServiceStatus.Ok;
            return Task.FromResult(result);
        }
        protected virtual CommandResult<TData> Ok(TData data)
        {
            result._data = data;
            result.Status = ApplicationServiceStatus.Ok;
            return result;
        }
        protected virtual Task<CommandResult<TData>> ResultAsync(TData data, ApplicationServiceStatus status)
        {
            result._data = data;
            result.Status = status;
            return Task.FromResult(result);
        }

        protected virtual CommandResult<TData> Result(TData data, ApplicationServiceStatus status)
        {
            result._data = data;
            result.Status = status;
            return result;
        }

        protected void AddMessage(string message)
        {
            result.AddMessage(_hamoonServices.Translator[message]);
        }
        protected void AddMessage(string message, params string[] arguments)
        {
            result.AddMessage(_hamoonServices.Translator[message, arguments]);
        }
    }

    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
    {
        protected readonly OKEServiceServices _hamoonServices;
        protected readonly CommandResult result = new CommandResult();
        public CommandHandler(OKEServiceServices hamoonServices)
        {
            _hamoonServices = hamoonServices;
        }
        public abstract Task<CommandResult> Handle(TCommand request);

        protected virtual Task<CommandResult> OkAsync()
        {
            result.Status = ApplicationServiceStatus.Ok;
            return Task.FromResult(result);
        }

        protected virtual CommandResult Ok()
        {
            result.Status = ApplicationServiceStatus.Ok;
            return result;
        }

        protected virtual Task<CommandResult> ResultAsync(ApplicationServiceStatus status)
        {
            result.Status = status;
            return Task.FromResult(result);
        }
        protected virtual CommandResult Result(ApplicationServiceStatus status)
        {
            result.Status = status;
            return result;
        }
        protected void AddMessage(string message)
        {
            result.AddMessage(_hamoonServices.Translator[message]);
        }
        protected void AddMessage(string message, params string[] arguments)
        {
            result.AddMessage(_hamoonServices.Translator[message, arguments]);
        }
    }
}
