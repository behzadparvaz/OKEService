using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Core.Domain.Exceptions
{
    public class InvalidValueObjectStateException : DomainStateException
    {
        public InvalidValueObjectStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}
