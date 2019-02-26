using System;
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace HN.Bangumi.API.Http
{
    internal class BangumiHttpClient : HttpClient
    {
        internal BangumiHttpClient(SignInManager signInManager, IOptions<BangumiOptions> bangumiOptionsAccesser) : base(new BangumiHttpClientHandler(signInManager, bangumiOptionsAccesser))
        {
            BaseAddress = new Uri(Constants.BangumiUrlBase);
        }
    }
}
