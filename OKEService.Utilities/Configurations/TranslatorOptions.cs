﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Utilities.Configurations
{
    public class TranslatorOptions
    {
        public string CultureInfo { get; set; } = "en-Us";
        public string TranslatorTypeName { get; set; }
        public ParrotTranslatorOptions Parrottranslator { get; set; }
        public MicrosoftTranslatorOptions MicrosoftTranslatorOptions { get; set; }
    }


    public class ParrotTranslatorOptions
    {
        public string ConnectionString { get; set; }
        public bool AutoCreateSqlTable { get; set; } = true;
        public string TableName { get; set; } = "ParrotTranslations";
        public string SchemaName { get; set; } = "dbo";
    }

    public class MicrosoftTranslatorOptions
    {
        public string ResourceKeyHolderAssemblyQualifiedTypeName { get; set; }
    }

}
