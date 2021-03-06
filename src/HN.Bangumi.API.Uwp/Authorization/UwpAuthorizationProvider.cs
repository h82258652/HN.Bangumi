﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Windows.Foundation;
using Windows.Security.Authentication.Web;

namespace HN.Bangumi.API.Authorization
{
    public class UwpAuthorizationProvider : AuthorizationProviderBase
    {
        public UwpAuthorizationProvider(IOptions<BangumiOptions> bangumiOptionsAccesser) : base(bangumiOptionsAccesser)
        {
        }

        protected override async Task<string> GetAuthorizationCodeAsync(Uri authorizationUri, Uri callbackUri)
        {
            var authorizationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, authorizationUri, callbackUri);
            switch (authorizationResult.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    var responseUri = new Uri(authorizationResult.ResponseData, UriKind.Absolute);
                    return new WwwFormUrlDecoder(responseUri.Query).GetFirstValueByName("code");

                case WebAuthenticationStatus.UserCancel:
                    throw new UserCancelAuthorizationException();

                case WebAuthenticationStatus.ErrorHttp:
                    throw new HttpErrorAuthorizationException();

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
