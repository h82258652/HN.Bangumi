using System;
using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.API
{
    public static partial class BangumiClientExtensions
    {
        public static Task<BangumiResult> UpdateStatusAsync(this IBangumiClient client, int epId, EpStatus status, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<BangumiResult>($"/ep/{epId}/status/{status.ToString().ToLower()}", cancellationToken);
        }
    }
}
