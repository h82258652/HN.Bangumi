using System;

namespace HN.Bangumi.API.Models
{
    public class AccessToken
    {
        public DateTime ExpiresAt { get; set; }

        public string RefreshToken { get; set; }

        public string Scope { get; set; }

        public string TokenType { get; set; }

        public long UserId { get; set; }

        public string Value { get; set; }
    }
}
