﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.API
{
    public static partial class BangumiClientExtensions
    {
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
            url += $"&responseGroup={responseGroup.ToString().ToLower()}&start={start}&max_results={maxResults}";

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
