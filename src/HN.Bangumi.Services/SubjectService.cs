using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IBangumiClient _client;

        public SubjectService(IBangumiClient client)
        {
            _client = client;
        }

        public Task<Subject> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.GetSubjectAsync(id, ResponseGroup.Large, cancellationToken);
        }

        public Task<SearchResult> SearchAnimeAsync(string query, int skip, int count, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.SearchAsync(query, skip, count, SubjectType.Anime, ResponseGroup.Large, cancellationToken);
        }

        public Task<SearchResult> SearchBookAsync(string query, int skip, int count, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.SearchAsync(query, skip, count, SubjectType.Book, ResponseGroup.Large, cancellationToken);
        }

        public Task<SearchResult> SearchGameAsync(string query, int skip, int count, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.SearchAsync(query, skip, count, SubjectType.Game, ResponseGroup.Large, cancellationToken);
        }

        public Task<SearchResult> SearchMusicAsync(string query, int skip, int count, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.SearchAsync(query, skip, count, SubjectType.Music, ResponseGroup.Large, cancellationToken);
        }

        public Task<SearchResult> SearchRealAsync(string query, int skip, int count, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.SearchAsync(query, skip, count, SubjectType.Real, ResponseGroup.Large, cancellationToken);
        }
    }
}
