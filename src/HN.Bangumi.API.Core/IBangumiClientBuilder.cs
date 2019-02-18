using Microsoft.Extensions.DependencyInjection;

namespace HN.Bangumi.API
{
    public interface IBangumiClientBuilder
    {
        IServiceCollection Services { get; }

        IBangumiClient Build();
    }
}
