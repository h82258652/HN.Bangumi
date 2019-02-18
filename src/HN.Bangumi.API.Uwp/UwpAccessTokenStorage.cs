using System;
using System.Linq;
using HN.Bangumi.API.Models;
using Windows.Security.Credentials;

namespace HN.Bangumi.API
{
    public class UwpAccessTokenStorage : IAccessTokenStorage
    {
        private const string BangumiPasswordVaultResourceName = "BangumiAccessToken";

        public void Clear()
        {
            var passwordVault = new PasswordVault();
            var passwordCredentials = passwordVault.FindAllByResource(BangumiPasswordVaultResourceName);
            foreach (var passwordCredential in passwordCredentials)
            {
                passwordVault.Remove(passwordCredential);
            }
        }

        public AccessToken Load()
        {
            var passwordVault = new PasswordVault();
            var passwordCredentials = passwordVault.RetrieveAll().Where(temp => temp.Resource == BangumiPasswordVaultResourceName).ToList();
            var accessTokenPasswordCredential = passwordCredentials.FirstOrDefault(temp => temp.UserName == "AccessToken");
            var expiresAtPasswordCredential = passwordCredentials.FirstOrDefault(temp => temp.UserName == "ExpiresAt");
            var tokenTypePasswordCredential = passwordCredentials.FirstOrDefault(temp => temp.UserName == "TokenType");
            var scopePasswordCredential = passwordCredentials.FirstOrDefault(temp => temp.UserName == "Scope");
            var refreshTokenPasswordCredential = passwordCredentials.FirstOrDefault(temp => temp.UserName == "RefreshToken");
            var userIdPasswordCredential = passwordCredentials.FirstOrDefault(temp => temp.UserName == "UserId");
            if (accessTokenPasswordCredential == null ||
                expiresAtPasswordCredential == null ||
                tokenTypePasswordCredential == null ||
                scopePasswordCredential == null ||
                refreshTokenPasswordCredential == null ||
                userIdPasswordCredential == null)
            {
                return null;
            }

            accessTokenPasswordCredential.RetrievePassword();
            expiresAtPasswordCredential.RetrievePassword();
            tokenTypePasswordCredential.RetrievePassword();
            scopePasswordCredential.RetrievePassword();
            refreshTokenPasswordCredential.RetrievePassword();
            userIdPasswordCredential.RetrievePassword();

            return new AccessToken
            {
                Value = accessTokenPasswordCredential.Password,
                ExpiresAt = DateTime.Parse(expiresAtPasswordCredential.Password),
                TokenType = tokenTypePasswordCredential.Password,
                Scope = scopePasswordCredential.Password,
                RefreshToken = refreshTokenPasswordCredential.Password,
                UserId = long.Parse(userIdPasswordCredential.Password)
            };
        }

        public void Save(AccessToken accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var passwordVault = new PasswordVault();
            passwordVault.Add(new PasswordCredential(BangumiPasswordVaultResourceName, "AccessToken", accessToken.Value));
            passwordVault.Add(new PasswordCredential(BangumiPasswordVaultResourceName, "ExpiresAt", accessToken.ExpiresAt.ToString("O")));
            passwordVault.Add(new PasswordCredential(BangumiPasswordVaultResourceName, "TokenType", accessToken.TokenType));
            passwordVault.Add(new PasswordCredential(BangumiPasswordVaultResourceName, "Scope", accessToken.Scope));
            passwordVault.Add(new PasswordCredential(BangumiPasswordVaultResourceName, "RefreshToken", accessToken.RefreshToken));
            passwordVault.Add(new PasswordCredential(BangumiPasswordVaultResourceName, "UserId", accessToken.UserId.ToString()));
        }
    }
}
