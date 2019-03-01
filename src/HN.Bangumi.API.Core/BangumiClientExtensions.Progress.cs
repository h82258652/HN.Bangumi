using System;
using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.API
{
    public static partial class BangumiClientExtensions
    {
        /// <summary>
        /// 更新收视进度
        /// </summary>
        /// <param name="client"></param>
        /// <param name="epId">章节 ID</param>
        /// <param name="status">收视类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<BangumiResult> UpdateStatusAsync(this IBangumiClient client, int epId, EpStatus status, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/ep/{epId}/status/{status.GetValue()}";
            return client.GetAsync<BangumiResult>(url, cancellationToken);
        }
    }
}
