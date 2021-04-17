using OKEService.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Core.ApplicationServices.Commands
{
    public class CommandResult : ApplicationServiceResult
    {

    }

    public class CommandResult<TData> : CommandResult
    {
        internal TData _data;
        public TData Data
        {
            get
            {
                return _data;
            }
        }

    }
}
