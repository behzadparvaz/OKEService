using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Utilities.Services.Logger
{
    public interface IScopeInformation
    {
        Dictionary<string, string> HostScopeInfo { get; }
        Dictionary<string, string> RequestScopeInfo { get; }
    }
}
