using System;
using HN.Bangumi.API.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HN.Bangumi.API
{
    public class BangumiClientBuilder : IBangumiClientBuilder
    {
        public IServiceCollection Services { get; }

        public BangumiClientBuilder()
        {
            Services = new ServiceCollection();
            Services.AddSingleton<IBangumiClient, BangumiClient>();
        }

        public IBangumiClient Build()
        {
            var serviceProvider = Services.BuildServiceProvider();
            if (serviceProvider.GetService<IOptions<BangumiOptions>>() == null)
            {
                throw new Exception("未进行配置，请调用 WithConfig 方法");
            }
            if (serviceProvider.GetService<IAuthorizationProvider>() == null)
            {
                throw new Exception("未配置 AuthorizationProvider");
            }
            if (serviceProvider.GetService<IAccessTokenStorage>() == null)
            {
                throw new Exception("未配置 AccessToken 存储");
            }

            return serviceProvider.GetRequiredService<IBangumiClient>();
        }
    }
}
