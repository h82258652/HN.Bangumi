using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.Services
{
    public interface IUserService
    {
        Task<User> GetAsync(long userId, CancellationToken cancellationToken = default(CancellationToken));

        Task<CollectionSubject[]> GetCollectionAsync(long userId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
