using System;
using HN.Bangumi.API.Authorization;

namespace HN.Bangumi.API
{
    public static class BangumiClientBuilderExtensions
    {
        public static IBangumiClientBuilder UseDefaultAccessTokenStorage(this IBangumiClientBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseAccessTokenStorage<UwpAccessTokenStorage>();
        }

        public static IBangumiClientBuilder UseDefaultAuthorizationProvider(this IBangumiClientBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.UseAuthorizationProvider<UwpAuthorizationProvider>();
            return builder;
        }
    }
}
