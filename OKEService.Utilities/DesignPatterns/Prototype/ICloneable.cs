using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Utilities.DesignPatterns.Prototype
{
    public interface ICloneable<T>
    {
        T Clone();
    }
}
