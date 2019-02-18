using System;
using System.Collections.Generic;
using System.Net.Http;
using HN.Bangumi.API.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Windows.ApplicationModel.Background;

namespace HN.Bangumi.API
{
    public abstract class RefreshTokenTaskBase : IBackgroundTask
    {
        private readonly IAccessTokenStorage _accessTokenStorage;
        private readonly BangumiOptions _bangumiOptions;

        protected RefreshTokenTaskBase(IOptions<BangumiOptions> bangumiOptions, IAccessTokenStorage accessTokenStorage)
        {
            _bangumiOptions = bangumiOptions.Value;
            _accessTokenStorage = accessTokenStorage;
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = null;

            try
            {
                deferral = taskInstance.GetDeferral();
                var accessToken = _accessTokenStorage.Load();
                if (accessToken == null)
                {
                    return;
                }

                using (var client = new HttpClient())
                {
                    var postData = new Dictionary<string, string>
                    {
                        ["grant_type"] = "refresh_token",
                        ["client_id"] = _bangumiOptions.AppKey,
                        ["client_secret"] = _bangumiOptions.AppSecret,
                        ["refresh_token"] = accessToken.RefreshToken,
                        ["redirect_uri"] = _bangumiOptions.RedirectUri
                    };

                    var postContent = new FormUrlEncodedContent(postData);

                    var requestTime = DateTime.Now;
                    var response = await client.PostAsync("https://bgm.tv/oauth/access_token", postContent);
                    var json = await response.Content.ReadAsStringAsync();
                    var authorizationResult = JsonConvert.DeserializeObject<AuthorizationResult>(json);

                    accessToken.Value = authorizationResult.AccessToken;
                    accessToken.ExpiresAt = requestTime.AddSeconds(authorizationResult.ExiresIn).AddMinutes(-5);
                    accessToken.TokenType = authorizationResult.TokenType;
                    accessToken.Scope = authorizationResult.Scope;
                    accessToken.RefreshToken = authorizationResult.RefreshToken;

                    _accessTokenStorage.Save(accessToken);
                }
            }
            catch
            {
                // ignored
            }
            finally
            {
                deferral?.Complete();
            }
        }
    }
}
