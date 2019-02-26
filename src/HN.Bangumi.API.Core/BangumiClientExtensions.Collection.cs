using System;
using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models.Preview;

namespace HN.Bangumi.API
{
    public static partial class BangumiClientExtensions
    {
        public static Task<Xx> GetCollectionAsync(this IBangumiClient client, int subjectId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var url = $"/collection/{subjectId}";
            return client.GetAsync<Xx>(url, cancellationToken);
        }
    }
}
