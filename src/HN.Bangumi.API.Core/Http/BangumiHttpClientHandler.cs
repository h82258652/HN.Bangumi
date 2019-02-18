using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace HN.Bangumi.API.Http
{
    internal class BangumiHttpClientHandler : DelegatingHandler
    {
        private readonly SignInManager _signInManager;

        internal BangumiHttpClientHandler(SignInManager signInManager) : base(new HttpClientHandler())
        {
            _signInManager = signInManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _signInManager.SignInAndGetAccessTokenAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue(accessToken.TokenType, accessToken.Value);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
