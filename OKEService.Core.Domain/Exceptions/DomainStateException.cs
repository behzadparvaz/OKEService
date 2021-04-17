using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Core.Domain.Exceptions
{
    public abstract class DomainStateException : Exception
    {
        public string[] Parameters { get; set; }

        public DomainStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}
