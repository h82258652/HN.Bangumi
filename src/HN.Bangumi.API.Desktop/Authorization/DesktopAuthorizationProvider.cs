using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Options;

namespace HN.Bangumi.API.Authorization
{
    public class DesktopAuthorizationProvider : AuthorizationProviderBase
    {
        public DesktopAuthorizationProvider(IOptions<BangumiOptions> bangumiOptionsAccesser) : base(bangumiOptionsAccesser)
        {
        }

        protected override Task<string> GetAuthorizationCodeAsync(Uri authorizationUri, Uri callbackUri)
        {
            var authorizationDialog = new AuthorizationDialog(authorizationUri);
            if (authorizationDialog.ShowDialog() == DialogResult.OK)
            {
                return Task.FromResult(authorizationDialog.AuthorizationCode);
            }
            else
            {
                if (authorizationDialog.IsHttpError)
                {
                    throw new HttpErrorAuthorizationException();
                }
                else
                {
                    throw new UserCancelAuthorizationException();
                }
            }
        }
    }
}
