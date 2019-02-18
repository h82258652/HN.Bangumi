using System;
using System.Threading.Tasks;
using HN.Bangumi.API.Authorization;
using Microsoft.Extensions.Options;

namespace HN.Bangumi.API
{
    internal class BangumiClient : IBangumiClient
    {
        private readonly SignInManager _signInManager;

        public BangumiClient(IOptions<BangumiOptions> bangumiOptions, IAuthorizationProvider authorizationProvider, IAccessTokenStorage accessTokenStorage)
        {
            _signInManager = new SignInManager(bangumiOptions, authorizationProvider, accessTokenStorage);
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
