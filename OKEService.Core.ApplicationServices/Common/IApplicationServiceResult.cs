using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Core.Domain.Common
{
    public interface IApplicationServiceResult
    {
        IEnumerable<string> Messages { get; }
        ApplicationServiceStatus Status { get; set; }
    }
}
