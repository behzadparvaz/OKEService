using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Core.Domain.Common
{
    public enum ApplicationServiceStatus
    {
        Ok = 1,
        NotFound = 2,
        ValidationError = 3,
        InvalidDomainState = 4,
        Exception = 5,
    }
}
