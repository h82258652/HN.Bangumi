using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.API
{
    public static partial class BangumiClientExtensions
    {
        /// <summary>
        /// 获取指定条目收藏信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="subjectId">条目 ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<SubjectCollectionStatus> GetCollectionStatusAsync(this IBangumiClient client, int subjectId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/collection/{subjectId}";
            return client.GetAsync<SubjectCollectionStatus>(url, cancellationToken);
        }

        /// <summary>
        /// 管理收藏
        /// </summary>
        /// <param name="client"></param>
        /// <param name="subjectId">条目 ID</param>
        /// <param name="status">收藏状态</param>
        /// <param name="comment">简评</param>
        /// <param name="tags">标签</param>
        /// <param name="rating">评分</param>
        /// <param name="privacy">收藏隐私</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<SubjectCollectionStatus> UpdateCollectionStatusAsync(this IBangumiClient client, int subjectId, CollectionStatus status, string comment = null, string[] tags = null, int? rating = null, bool privacy = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var postData = new Dictionary<string, string>
            {
                ["status"] = status.GetValue()
            };
            if (comment != null)
            {
                postData["comment"] = comment;
            }
            if (tags != null)
            {
                postData["tags"] = string.Join(" ", tags);
            }
            if (rating.HasValue)
            {
                postData["rating"] = rating.Value.ToString();
            }
            postData["privacy"] = privacy ? "1" : "0";

            var postContent = new FormUrlEncodedContent(postData);

            var url = $"/collection/{subjectId}/update";
            return client.PostAsync<SubjectCollectionStatus>(url, postContent, cancellationToken);
        }
    }
}
