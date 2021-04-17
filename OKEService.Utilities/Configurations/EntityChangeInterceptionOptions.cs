using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Utilities.Configurations
{
    public class EntityChangeInterceptionOptions
    {
        public bool Enabled { get; set; }
        public string EntityChageInterceptorRepositoryTypeName { get; set; }
    }
}
