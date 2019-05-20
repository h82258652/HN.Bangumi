using System;
using System.Net;
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

        internal BangumiHttpClientHandler(SignInManager signInManager, IOptions<BangumiOptions> bangumiOptionsAccesser) : base(CreateInnerHandler())
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
                .OrResult(response => response.Content.Headers.ContentType.MediaType == "text/html")
                .WaitAndRetryAsync(retryCount, count => retryDelay);

            return await policy.ExecuteAsync(() => base.SendAsync(request, cancellationToken));
        }

        private static HttpMessageHandler CreateInnerHandler()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(new Cookie("chii_searchDateLine", "0"));
            return new HttpClientHandler
            {
                CookieContainer = cookieContainer
            };
        }
    }
}
