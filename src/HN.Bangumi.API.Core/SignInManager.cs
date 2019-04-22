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

        internal SignInManager(IOptions<BangumiOptions> bangumiOptionsAccesser, IAuthorizationProvider authorizationProvider, IAccessTokenStorage accessTokenStorage)
        {
            _bangumiOptions = bangumiOptionsAccesser.Value;
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

        internal async Task RefreshAsync()
        {
            var accessToken = _accessTokenStorage.Load();
            var refreshToken = accessToken?.RefreshToken;
            if (refreshToken == null)
            {
                return;
            }

            var requestTime = DateTime.Now;

            var refreshResult = await _authorizationProvider.RefreshAsync(refreshToken);

            accessToken = new AccessToken
            {
                Value = refreshResult.AccessToken,
                ExpiresAt = requestTime.AddSeconds(refreshResult.ExiresIn).AddMinutes(-5),// 5 分钟用作缓冲
                TokenType = refreshResult.TokenType,
                Scope = refreshResult.Scope,
                RefreshToken = refreshResult.RefreshToken,
                UserId = accessToken.UserId
            };
            _accessTokenStorage.Save(accessToken);
        }

        internal async Task SignInAsync()
        {
            var accessToken = GetAccessToken();
            if (accessToken != null)
            {
                return;
            }

            var requestTime = DateTime.Now;
            var authorizationUri = new Uri($"https://bgm.tv/oauth/authorize?client_id={_bangumiOptions.AppKey}&response_type=code&redirect_uri={_bangumiOptions.RedirectUri}");
            var authorizationResult = await _authorizationProvider.AuthorizeAsync(authorizationUri, new Uri(_bangumiOptions.RedirectUri));

            accessToken = new AccessToken
            {
                Value = authorizationResult.AccessToken,
                ExpiresAt = requestTime.AddSeconds(authorizationResult.ExiresIn).AddMinutes(-5),// 5 分钟用作缓冲
                TokenType = authorizationResult.TokenType,
                Scope = authorizationResult.Scope,
                RefreshToken = authorizationResult.RefreshToken,
                UserId = authorizationResult.UserId
            };
            _accessTokenStorage.Save(accessToken);
        }

        internal Task SignOutAsync()
        {
            _accessTokenStorage.Clear();
            return Task.CompletedTask;
        }
    }
}
