using System;
using System.Threading.Tasks;
using HN.Bangumi.API.Models;

namespace HN.Bangumi.API.Authorization
{
    public interface IAuthorizationProvider
    {
        Task<AuthorizationResult> AuthorizeAsync(Uri authorizationUri, Uri callbackUri);

        Task<AuthorizationResult> RefreshAsync(string refreshToken);
    }
}
