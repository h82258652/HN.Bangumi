using System;
using Windows.ApplicationModel.Background;
using HN.Bangumi.API;
using Microsoft.Extensions.Options;

namespace HN.Bangumi.Uwp.BackgroundTasks
{
    public sealed class RefreshTokenTask : IBackgroundTask
    {
        public RefreshTokenTask()
        {
            
        }

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            throw new NotImplementedException();
        }
    }
}
