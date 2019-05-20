using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HN.Bangumi.Uwp.Services
{
    public class BangumiHttpHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var innerHandler = new HttpClientHandler();
            InnerHandler = innerHandler;
            using (innerHandler)
            {
                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}
