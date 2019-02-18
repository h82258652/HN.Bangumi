using System;
using System.Threading.Tasks;
using HN.Bangumi.API.Authorization;
using HN.Bangumi.API.Models;
using Microsoft.Extensions.Options;

namespace HN.Bangumi.API
{
    internal class SignInManager
    {
        private readonly IAccessTokenStorage _accessTokenStorage;
        private readonly IAuthorizationProvider _authorizationProvider;
        private readonly BangumiOptions _bangumiOptions;

        internal SignInManager(IOptions<BangumiOptions> bangumiOptions, IAuthorizationProvider authorizationProvider, IAccessTokenStorage accessTokenStorage)
        {
            _bangumiOptions = bangumiOptions.Value;
            _authorizationProvider = authorizationProvider;
            _accessTokenStorage = accessTokenStorage;
        }

        internal bool IsSignIn => GetAccessToken() != null;

        internal AccessToken GetAccessToken()
        {
            var accessToken = _accessTokenStorage.Load();
            if (accessToken == null)
            {
                return null;
            }

            if (accessToken.ExpiresAt <= DateTime.Now)
            {
                _accessTokenStorage.Clear();
                return null;
            }

            return accessToken;
        }

        internal async Task<AccessToken> SignInAndGetAccessTokenAsync()
        {
            var accessToken = GetAccessToken();
            if (accessToken != null)
            {
                return accessToken;
            }

            var requestTime = DateTime.Now;
            var authorizationUri = new Uri($"https://bgm.tv/oauth/authorize?client_id={_bangumiOptions.AppKey}&response_type=code&redirect_uri={_bangumiOptions.RedirectUri}");
            var authorizationResult = await _authorizationProvider.AuthorizeAsync(authorizationUri, new Uri(_bangumiOptions.RedirectUri));

            accessToken = new AccessToken
            {
                ExpiresAt = requestTime.AddSeconds(authorizationResult.ExiresIn).AddMinutes(-5),// 5 分钟用作缓冲
                UserId = authorizationResult.UserId,
                Value = authorizationResult.AccessToken,
                RefreshToken = authorizationResult.RefreshToken
            };
            _accessTokenStorage.Save(accessToken);
            return accessToken;
        }

        internal Task SignInAsync()
        {
            return SignInAndGetAccessTokenAsync();
        }

        internal Task SignOutAsync()
        {
            _accessTokenStorage.Clear();
            return Task.CompletedTask;
        }
    }
}
