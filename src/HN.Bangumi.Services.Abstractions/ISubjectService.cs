using System.Threading;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.Services
{
    public interface ISubjectService
    {
        Task<Subject> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task<SubjectProgress> GetProgressAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task<SearchResult> SearchAnimeAsync(string query, int skip, int count, CancellationToken cancellationToken = default(CancellationToken));

        Task<SearchResult> SearchBookAsync(string query, int skip, int count, CancellationToken cancellationToken = default(CancellationToken));

        Task<SearchResult> SearchGameAsync(string query, int skip, int count, CancellationToken cancellationToken = default(CancellationToken));

        Task<SearchResult> SearchMusicAsync(string query, int skip, int count, CancellationToken cancellationToken = default(CancellationToken));

        Task<SearchResult> SearchRealAsync(string query, int skip, int count, CancellationToken cancellationToken = default(CancellationToken));
    }
}
