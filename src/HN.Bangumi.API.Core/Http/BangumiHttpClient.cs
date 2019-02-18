using System;
using System.Net.Http;

namespace HN.Bangumi.API.Http
{
    internal class BangumiHttpClient : HttpClient
    {
        internal BangumiHttpClient(SignInManager signInManager) : base(new BangumiHttpClientHandler(signInManager))
        {
            BaseAddress = new Uri(Constants.BangumiUrlBase);
        }
    }
}
