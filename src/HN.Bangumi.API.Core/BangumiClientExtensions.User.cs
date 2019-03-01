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
        /// 获取用户某个条目收视进度
        /// </summary>
        /// <param name="client"></param>
        /// <param name="username">用户名</param>
        /// <param name="subjectId">条目 ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<SubjectProgress> GetSubjectProgressAsync(this IBangumiClient client, string username, int subjectId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            var url = $"/user/{WebUtility.UrlEncode(username)}/progress?subject_id={subjectId}";
            return client.GetAsync<SubjectProgress>(url, cancellationToken);
        }

        /// <summary>
        /// 获取用户某个条目收视进度
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uid">UID</param>
        /// <param name="subjectId">条目 ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<SubjectProgress> GetSubjectProgressAsync(this IBangumiClient client, long uid, int subjectId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/user/{uid}/progress?subject_id={subjectId}";
            return client.GetAsync<SubjectProgress>(url, cancellationToken);
        }

        /// <summary>
        /// 获取用户基础信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="username">用户名</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<User> GetUserAsync(this IBangumiClient client, string username, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            var url = $"/user/{WebUtility.UrlEncode(username)}";
            return client.GetAsync<User>(url, cancellationToken);
        }

        /// <summary>
        /// 获取用户基础信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uid">UID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<User> GetUserAsync(this IBangumiClient client, long uid, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/user/{uid}";
            return client.GetAsync<User>(url, cancellationToken);
        }

        /// <summary>
        /// 获取用户收藏列表
        /// </summary>
        /// <param name="client"></param>
        /// <param name="username">用户名</param>
        /// <param name="cat">收藏类型</param>
        /// <param name="ids">收藏条目 ID</param>
        /// <param name="responseGroup"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Subject[]> GetUserCollectionAsync(
            this IBangumiClient client, 
            string username,
            CollectionType cat, 
            int[] ids = null, 
            ResponseGroup responseGroup = ResponseGroup.Medium,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }
            if (responseGroup == ResponseGroup.Large)
            {
                throw new NotSupportedException();
            }

            var url = $"/user/{WebUtility.UrlEncode(username)}/collection?cat={cat.GetValue()}";
            if (ids != null)
            {
                url += $"&ids={string.Join(",", ids)}";
            }
            url += $"&responseGroup={responseGroup.GetValue()}";
            return client.GetAsync<Subject[]>(url, cancellationToken);
        }

        /// <summary>
        /// 获取用户收藏列表
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uid">UID</param>
        /// <param name="cat">收藏类型</param>
        /// <param name="ids">收藏条目 ID</param>
        /// <param name="responseGroup"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<CollectionSubject[]> GetUserCollectionAsync(
            this IBangumiClient client, 
            long uid, 
            CollectionType cat, 
            int[] ids = null, 
            ResponseGroup responseGroup = ResponseGroup.Medium, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (responseGroup == ResponseGroup.Large)
            {
                throw new NotSupportedException();
            }

            var url = $"/user/{uid}/collection?cat={cat.GetValue()}";
            if (ids != null)
            {
                url += $"&ids={string.Join(",", ids)}";
            }
            url += $"&responseGroup={responseGroup.GetValue()}";
            return client.GetAsync<CollectionSubject[]>(url, cancellationToken);
        }

        /// <summary>
        /// 获取用户指定类型的收藏概览，固定返回最近更新的收藏，不支持翻页
        /// </summary>
        /// <param name="client"></param>
        /// <param name="username">用户名</param>
        /// <param name="subjectType">条目类型</param>
        /// <param name="maxResults">显示条数</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<CollectionOverview[]> GetUserCollectionsOverviewAsync(this IBangumiClient client, string username, SubjectType subjectType, int maxResults, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            var url = $"/user/{WebUtility.UrlEncode(username)}/collections/{subjectType.GetValue()}?max_results={maxResults}";
            return client.GetAsync<CollectionOverview[]>(url, cancellationToken);
        }

        /// <summary>
        /// 获取用户指定类型的收藏概览，固定返回最近更新的收藏，不支持翻页
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uid"></param>
        /// <param name="subjectType"></param>
        /// <param name="maxResults"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<CollectionOverview[]> GetUserCollectionsOverviewAsync(this IBangumiClient client, long uid, SubjectType subjectType, int maxResults, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/user/{uid}/collections/{subjectType.GetValue()}?max_results={maxResults}";
            return client.GetAsync<CollectionOverview[]>(url, cancellationToken);
        }

        /// <summary>
        /// 获取用户所有收藏信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="username">用户名</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<UserCollectionStatus[]> GetUserCollectionStatusesAsync(this IBangumiClient client, string username, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            var url = $"/user/{WebUtility.UrlEncode(username)}/collections/status";
            return client.GetAsync<UserCollectionStatus[]>(url, cancellationToken);
        }

        /// <summary>
        /// 获取用户所有收藏信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uid">UID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<UserCollectionStatus[]> GetUserCollectionStatusesAsync(this IBangumiClient client, long uid, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/user/{uid}/collections/status";
            return client.GetAsync<UserCollectionStatus[]>(url, cancellationToken);
        }
    }
}
