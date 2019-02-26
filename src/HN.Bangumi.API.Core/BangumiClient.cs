using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Authorization;
using HN.Bangumi.API.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HN.Bangumi.API
{
    internal class BangumiClient : IBangumiClient
    {
        private readonly IOptions<BangumiOptions> _bangumiOptionsAccesser;
        private readonly SignInManager _signInManager;

        public BangumiClient(IOptions<BangumiOptions> bangumiOptionsAccesser, IAuthorizationProvider authorizationProvider, IAccessTokenStorage accessTokenStorage)
        {
            _bangumiOptionsAccesser = bangumiOptionsAccesser;
            _signInManager = new SignInManager(bangumiOptionsAccesser, authorizationProvider, accessTokenStorage);
        }

        public bool IsSignIn => _signInManager.IsSignIn;

        public long UserId
        {
            get
            {
                var accessToken = _signInManager.GetAccessToken();
                if (accessToken == null)
                {
                    throw new InvalidOperationException("用户未登录");
                }

                return accessToken.UserId;
            }
        }

        public Task<T> GetAsync<T>(string uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, uri), cancellationToken);
        }

        public Task<T> PostAsync<T>(string uri, HttpContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendAsync<T>(new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            }, cancellationToken);
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            using (var client = new BangumiHttpClient(_signInManager, _bangumiOptionsAccesser))
            {
                var response = await client.SendAsync(request, cancellationToken);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public Task SignInAsync()
        {
            return _signInManager.SignInAsync();
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }
}
