using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HN.Bangumi.API.Authorization
{
    public abstract class AuthorizationProviderBase : IAuthorizationProvider
    {
        private readonly BangumiOptions _bangumiOptions;

        protected AuthorizationProviderBase(IOptions<BangumiOptions> bangumiOptions)
        {
            _bangumiOptions = bangumiOptions.Value;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(Uri authorizationUri, Uri callbackUri)
        {
            var authorizationCode = await GetAuthorizationCodeAsync(authorizationUri, callbackUri);
            using (var client = new HttpClient())
            {
                var postData = new Dictionary<string, string>
                {
                    ["grant_type"] = "authorization_code",
                    ["client_id"] = _bangumiOptions.AppKey,
                    ["client_secret"] = _bangumiOptions.AppSecret,
                    ["code"] = authorizationCode,
                    ["redirect_uri"] = _bangumiOptions.RedirectUri
                };

                var postContent = new FormUrlEncodedContent(postData);

                var response = await client.PostAsync("https://bgm.tv/oauth/access_token", postContent);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthorizationResult>(json);
            }
        }

        protected abstract Task<string> GetAuthorizationCodeAsync(Uri authorizationUri, Uri callbackUri);
    }
}
