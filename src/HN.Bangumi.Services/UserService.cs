using System;
using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.Services
{
    public class UserService : IUserService
    {
        private readonly IBangumiClient _client;

        public UserService(IBangumiClient client)
        {
            _client = client;
        }

        public Task<object> X(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserAsync(long userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.GetUserAsync(userId, cancellationToken);
        }
    }
}
