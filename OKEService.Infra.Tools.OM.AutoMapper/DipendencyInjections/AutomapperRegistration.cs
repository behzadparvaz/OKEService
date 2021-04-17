using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OKEService.Infra.Tools.OM.AutoMapper.Common;
using OKEService.Utilities.Services.ObjectMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OKEService.Infra.Tools.OM.AutoMapper.DipendencyInjections
{
    public static class AutomapperRegistration
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var profileTypes = assemblies
                .SelectMany(x => x.DefinedTypes)
                .Where(type => typeof(Profile).IsAssignableFrom(type)).ToList();
            var profiles = new List<Profile>();
            foreach (var profileType in profileTypes)
            {
                profiles.Add((Profile)Activator.CreateInstance(profileType));
            }
            return services.AddSingleton<IMapperAdapter>(new AutoMapperAdapter(profiles.ToArray()));
        }
    }
}
