using Microsoft.Extensions.Logging;
using OKEService.Utilities.Services.Chaching;
using OKEService.Utilities.Services.ObjectMappers;
using OKEService.Utilities.Services.Serializers;
using OKEService.Utilities.Services.Translations;
using OKEService.Utilities.Services.Users;
using System;

namespace OKEService.Utilities
{
    public class OKEServiceServices
    {
        public readonly ITranslator Translator;
        public readonly ICacheAdapter CacheAdapter;
        public readonly IMapperAdapter MapperFacade;
        public readonly ILoggerFactory LoggerFactory;
        public readonly IJsonSerializer Serializer;
        public readonly IUserInfoService UserInfoService;

        public OKEServiceServices(ITranslator translator,
                ILoggerFactory loggerFactory,
                IJsonSerializer serializer,
                IUserInfoService userInfoService,
                ICacheAdapter cacheAdapter,
                IMapperAdapter mapperFacade)
        {
            Translator = translator;
            LoggerFactory = loggerFactory;
            Serializer = serializer;
            UserInfoService = userInfoService;
            CacheAdapter = cacheAdapter;
            MapperFacade = mapperFacade;
        }
    }
}
