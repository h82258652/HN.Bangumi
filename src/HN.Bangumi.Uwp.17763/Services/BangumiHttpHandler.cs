using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HN.Bangumi.Uwp.Services
{
    public class BangumiHttpHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            InnerHandler = new HttpClientHandler();
            return base.SendAsync(request, cancellationToken);
        }
    }
}
