using System;
using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;
using HN.Bangumi.API.Models.Preview;

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

        public static Task<Subject> GetSubjectAsync(this IBangumiClient client, int id, ResponseGroup responseGroup = ResponseGroup.Small, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/subject/{id}?responseGroup={responseGroup.ToString().ToLower()}";
            return client.GetAsync<Subject>(url, cancellationToken);
        }
    }
}
