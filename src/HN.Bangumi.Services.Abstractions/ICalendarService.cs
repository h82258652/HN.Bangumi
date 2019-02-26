using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.Services
{
    public interface ICalendarService
    {
        Task<Calendar[]> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
