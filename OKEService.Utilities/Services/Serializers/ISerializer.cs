using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Utilities.Services.Serializers
{
    public interface IJsonSerializer
    {
        string Serilize<TInput>(TInput input);
        TOutput Deserialize<TOutput>(string input);
        object Deserialize(string input, Type type);
    }
}
