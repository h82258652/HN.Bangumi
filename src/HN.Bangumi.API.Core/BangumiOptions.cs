using System;

namespace HN.Bangumi.API
{
    public class BangumiOptions
    {
        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public string RedirectUri { get; set; }

        public int RetryCount { get; set; }

        public TimeSpan RetryDelay { get; set; } = TimeSpan.Zero;
    }
}
