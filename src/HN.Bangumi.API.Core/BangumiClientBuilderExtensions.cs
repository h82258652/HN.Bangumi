using System;
using HN.Bangumi.API.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace HN.Bangumi.API
{
    public static class BangumiClientBuilderExtensions
    {
        public static IBangumiClientBuilder UseAccessTokenStorage<T>(this IBangumiClientBuilder builder) where T : class, IAccessTokenStorage
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddTransient<IAccessTokenStorage, T>();
            return builder;
        }

        public static IBangumiClientBuilder UseAuthorizationProvider<T>(this IBangumiClientBuilder builder) where T : class, IAuthorizationProvider
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddTransient<IAuthorizationProvider, T>();
            return builder;
        }

        public static IBangumiClientBuilder WithConfig(this IBangumiClientBuilder builder, Action<BangumiOptions> configureOptions)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            builder.Services.Configure(configureOptions);
            return builder;
        }

        public static IBangumiClientBuilder WithConfig(this IBangumiClientBuilder builder, string appKey, string appSecret, string redirectUri)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(appKey))
            {
                throw new ArgumentException("AppKey 为空");
            }
            if (string.IsNullOrEmpty(appSecret))
            {
                throw new ArgumentException("AppSecret 为空");
            }
            if (string.IsNullOrEmpty(redirectUri))
            {
                throw new ArgumentException("RedirectUri 为空");
            }

            return builder.WithConfig(options =>
            {
                options.AppKey = appKey;
                options.AppSecret = appSecret;
                options.RedirectUri = redirectUri;
            });
        }
    }
}
