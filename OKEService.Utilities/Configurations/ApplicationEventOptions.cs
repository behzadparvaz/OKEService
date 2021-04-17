using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Utilities.Configurations
{
    public class ApplicationEventOptions
    {
        public bool TransactionalEventsEnabled { get; set; }
        public bool RaiseInmemoryEvents { get; set; }
    }
}
