using System;
using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.API
{
    public static partial class BangumiClientExtensions
    {
        /// <summary>
        /// 获取每日放送
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Calendar[]> GetCalendarAsync(this IBangumiClient client, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            const string url = "/calendar";
            return client.GetAsync<Calendar[]>(url, cancellationToken);
        }

        /// <summary>
        /// 获取条目信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="id">条目 ID</param>
        /// <param name="responseGroup">返回数据大小</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Subject> GetSubjectAsync(this IBangumiClient client, int id, ResponseGroup responseGroup = ResponseGroup.Small, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/subject/{id}?responseGroup={responseGroup.GetValue()}";
            return client.GetAsync<Subject>(url, cancellationToken);
        }

        /// <summary>
        /// 获取章节数据
        /// </summary>
        /// <param name="client"></param>
        /// <param name="id">条目 ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Subject> GetSubjectEpInfoAsync(this IBangumiClient client, int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/subject/{id}/ep";
            return client.GetAsync<Subject>(url, cancellationToken);
        }
    }
}
