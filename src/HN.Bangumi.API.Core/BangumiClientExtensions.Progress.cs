using System;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.API
{
    public static partial class BangumiClientExtensions
    {
        public static Task<BangumiResult> UpdateStatus(this IBangumiClient client, int epId, EpStatus status)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.GetAsync<BangumiResult>($"/ep/{epId}/status/{status.ToString().ToLower()}");
        }
    }
}
