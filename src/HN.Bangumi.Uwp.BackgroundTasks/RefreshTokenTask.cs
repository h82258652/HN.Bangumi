using System;
using HN.Bangumi.API;
using Windows.ApplicationModel.Background;
using HN.Bangumi.Configuration;

namespace HN.Bangumi.Uwp.BackgroundTasks
{
    public sealed class RefreshTokenTask : IBackgroundTask
    {
        private readonly IBangumiClient _bangumiClient;

        public RefreshTokenTask()
        {
            var appConfiguration = new AppConfiguration();
            _bangumiClient = new BangumiClientBuilder()
                .WithConfig(options =>
                {
                    options.AppKey = appConfiguration.AppKey;
                    options.AppSecret = appConfiguration.AppSecret;
                    options.RedirectUri = appConfiguration.RedirectUri;
                    options.RetryCount = 3;
                    options.RetryDelay = TimeSpan.FromSeconds(1);
                })
                .UseDefaultAuthorizationProvider()
                .UseDefaultAccessTokenStorage()
                .Build();
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = null;

            try
            {
                deferral = taskInstance.GetDeferral();

                await _bangumiClient.RefreshTokenAsync();
            }
            catch
            {
                // ignored
            }
            finally
            {
                deferral?.Complete();
            }
        }
    }
}
