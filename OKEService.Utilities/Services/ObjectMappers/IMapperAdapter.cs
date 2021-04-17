using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Utilities.Services.ObjectMappers
{
    public interface IMapperAdapter
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
