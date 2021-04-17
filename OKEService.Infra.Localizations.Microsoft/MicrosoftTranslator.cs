using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using OKEService.Utilities.Services.Translations;
using System;

namespace OKEService.Infra.Localizations.Microsoft
{
    public class MicrosoftTranslator : ITranslator
    {
        private readonly IStringLocalizer _localizer;
        public MicrosoftTranslator(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            using var serviceScop = serviceScopeFactory.CreateScope();
            var stringLocalizerFactory = serviceScop.ServiceProvider.GetRequiredService<IStringLocalizerFactory>();
            var assemblyQualifiedName = configuration["OKEServiceConfigurations:MicrosoftTranslator:ResourceKeyHolderAssemblyQualifiedTypeName"];
            var type = Type.GetType(assemblyQualifiedName);
            _localizer = stringLocalizerFactory.Create(type);
        }
        public string this[string name] { get => GetString(name); set => throw new NotImplementedException(); }
        public string this[string name, params string[] arguments] { get => GetString(name, arguments); set => throw new NotImplementedException(); }
        public string this[char sepprator, params string[] names] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetString(string name)
        {
            return _localizer[name];
        }
        public string GetString(string name, params string[] arguments)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                arguments[i] = GetString(arguments[i]);
            }
            return _localizer[name, arguments];
        }

        public string GetConcateString(char sepprator, params string[] names)
        {
            throw new NotImplementedException();
        }
    }
}
