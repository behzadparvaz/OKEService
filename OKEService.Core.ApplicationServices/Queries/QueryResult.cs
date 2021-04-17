using OKEService.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Core.ApplicationServices.Queries
{
    public sealed class QueryResult<TData> : ApplicationServiceResult
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
