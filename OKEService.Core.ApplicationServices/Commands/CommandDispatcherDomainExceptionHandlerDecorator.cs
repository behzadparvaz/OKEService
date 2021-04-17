﻿using OKEService.Core.Domain.Common;
using OKEService.Core.Domain.Exceptions;
using OKEService.Utilities.Services.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKEService.Core.ApplicationServices.Commands
{
    public class CommandDispatcherDomainExceptionHandlerDecorator : CommandDispatcherDecorator
    {
        private readonly ITranslator _translator;

        public CommandDispatcherDomainExceptionHandlerDecorator(CommandDispatcher commandDispatcher, ITranslator translator) : base(commandDispatcher)
        {
            _translator = translator;
        }

        public override Task<CommandResult> Send<TCommand>(in TCommand command)
        {
            try
            {
                return _commandDispatcher.Send(command);
            }
            catch (DomainStateException ex)
            {

                return DomainExceptionHandling<TCommand, CommandResult>(ex);
            }

        }

        public override Task<CommandResult<TData>> Send<TCommand, TData>(in TCommand command)
        {
            try
            {
                return _commandDispatcher.Send<TCommand, TData>(command);
            }
            catch (DomainStateException ex)
            {
                return DomainExceptionHandling<TCommand, CommandResult<TData>>(ex);
            }

        }

        private Task<TCommandResult> DomainExceptionHandling<TCommand, TCommandResult>(DomainStateException ex) where TCommandResult : ApplicationServiceResult, new()
        {
            var type = typeof(TCommandResult);
            dynamic commandResult = new CommandResult();
            if (type.IsGenericType)
            {
                var d1 = typeof(CommandResult<>);
                var makeme = d1.MakeGenericType(type.GetGenericArguments());
                commandResult = Activator.CreateInstance(makeme);
            }
            if (ex?.Parameters.Any() == true)
            {
                commandResult.AddMessage(_translator[ex.Message, ex?.Parameters]);
            }
            else
                commandResult.AddMessage(_translator[ex.Message]);

            commandResult.Status = ApplicationServiceStatus.InvalidDomainState;
            return Task.FromResult(commandResult as TCommandResult);
        }
    }
}
