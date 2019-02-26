using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IBangumiClient _client;

        public CalendarService(IBangumiClient client)
        {
            _client = client;
        }

        public Task<Calendar[]> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.GetCalendarAsync(cancellationToken);
        }
    }
}
