using System.Threading.Tasks;

namespace HN.Bangumi.API
{
    public interface IBangumiClient
    {
        bool IsSignIn { get; }

        long UserId { get; }

        Task SignInAsync();

        Task SignOutAsync();
    }
}
