using HN.Bangumi.API.Models;

namespace HN.Bangumi.API
{
    public interface IAccessTokenStorage
    {
        void Clear();

        AccessToken Load();

        void Save(AccessToken accessToken);
    }
}
