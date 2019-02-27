using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;

namespace HN.Bangumi.API.Http
{
    internal class BangumiHttpClientHandler : DelegatingHandler
    {
        private readonly BangumiOptions _bangumiOptions;
        private readonly SignInManager _signInManager;

        internal BangumiHttpClientHandler(SignInManager signInManager, IOptions<BangumiOptions> bangumiOptionsAccesser) : base(new HttpClientHandler())
        {
            _signInManager = signInManager;
            _bangumiOptions = bangumiOptionsAccesser.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(request.RequestUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["app_id"] = _bangumiOptions.AppKey;
            uriBuilder.Query = query.ToString();
            request.RequestUri = uriBuilder.Uri;

            var accessToken = _signInManager.GetAccessToken();
            if (accessToken != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(accessToken.TokenType, accessToken.Value);
            }

            var retryCount = _bangumiOptions.RetryCount;
            var retryDelay = _bangumiOptions.RetryDelay;
            var policy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(retryCount, count => retryDelay);

            return await policy.ExecuteAsync(() => base.SendAsync(request, cancellationToken));
        }
    }
}
