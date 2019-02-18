using System;
using HN.Bangumi.API;
using Microsoft.Extensions.Options;

namespace HN.Bangumi.Uwp.BackgroundTasks
{
    internal sealed class RefreshTokenTask : RefreshTokenTaskBase
    {
        public RefreshTokenTask(IOptions<BangumiOptions> bangumiOptions, IAccessTokenStorage accessTokenStorage) : base(bangumiOptions, accessTokenStorage)
        {
            throw new NotImplementedException();
        }
    }
}
