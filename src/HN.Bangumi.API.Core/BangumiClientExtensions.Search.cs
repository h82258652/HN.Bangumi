using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.API
{
    public static partial class BangumiClientExtensions
    {
        /// <summary>
        /// 条目搜索
        /// </summary>
        /// <param name="client"></param>
        /// <param name="keywords">关键词</param>
        /// <param name="start">开始条数</param>
        /// <param name="maxResults">每页条数</param>
        /// <param name="type">类型</param>
        /// <param name="responseGroup">返回数据大小</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<SearchResult> SearchAsync(this IBangumiClient client, string keywords, int start, int maxResults, SubjectType? type = null, ResponseGroup responseGroup = ResponseGroup.Small, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/search/subject/{WebUtility.UrlEncode(keywords)}?";
            if (type.HasValue)
            {
                url += $"type={(int)type}";
            }
            url += $"&responseGroup={responseGroup.GetValue()}&start={start}&max_results={maxResults}";

            var result = await client.GetAsync<SearchResult>(url, cancellationToken);
            if (result.ErrorCode == (int)HttpStatusCode.NotFound)
            {
                result = new SearchResult
                {
                    List = Array.Empty<Subject>()
                };
            }

            return result;
        }
    }
}
