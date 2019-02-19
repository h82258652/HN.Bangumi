using System;
using HN.Bangumi.API.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace HN.Bangumi.API
{
    public class DesktopAccessTokenStorage : IAccessTokenStorage
    {
        private const string BangumiFileName = "bangumi";
        private static readonly ISettings Settings = CrossSettings.Current;

        public void Clear()
        {
            Settings.Remove("AccessToken", BangumiFileName);
            Settings.Remove("ExpiresAt", BangumiFileName);
            Settings.Remove("TokenType", BangumiFileName);
            Settings.Remove("Scope", BangumiFileName);
            Settings.Remove("RefreshToken", BangumiFileName);
            Settings.Remove("UserId", BangumiFileName);
        }

        public AccessToken Load()
        {
            var value = Settings.GetValueOrDefault("AccessToken", null, BangumiFileName);
            var expiresAt = Settings.GetValueOrDefault("ExpiresAt", DateTime.MinValue, BangumiFileName);
            var tokenType = Settings.GetValueOrDefault("TokenType", null, BangumiFileName);
            var scope = Settings.GetValueOrDefault("Scope", null, BangumiFileName);
            var refreshToken = Settings.GetValueOrDefault("RefreshToken", null, BangumiFileName);
            var userId = Settings.GetValueOrDefault("UserId", 0L, BangumiFileName);

            return new AccessToken
            {
                Value = value,
                ExpiresAt = expiresAt,
                TokenType = tokenType,
                Scope = scope,
                RefreshToken = refreshToken,
                UserId = userId
            };
        }

        public void Save(AccessToken accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            Settings.AddOrUpdateValue("AccessToken", accessToken.Value, BangumiFileName);
            Settings.AddOrUpdateValue("ExpiresAt", accessToken.ExpiresAt, BangumiFileName);
            Settings.AddOrUpdateValue("TokenType", accessToken.TokenType, BangumiFileName);
            Settings.AddOrUpdateValue("Scope", accessToken.Scope, BangumiFileName);
            Settings.AddOrUpdateValue("RefreshToken", accessToken.RefreshToken, BangumiFileName);
            Settings.AddOrUpdateValue("UserId", accessToken.UserId, BangumiFileName);
        }
    }
}
