using System;
using System.Web;
using System.Windows.Forms;
using Microsoft.Win32;

namespace HN.Bangumi.API.Authorization
{
    public partial class AuthorizationDialog : Form
    {
        private readonly Uri _authorizationUri;

        static AuthorizationDialog()
        {
            if (!Environment.Is64BitOperatingSystem)
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", Application.ExecutablePath, 11001);
            }
            else
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", Application.ExecutablePath, 11001);
            }
        }

        public AuthorizationDialog(Uri authorizationUri)
        {
            if (authorizationUri == null)
            {
                throw new ArgumentNullException(nameof(authorizationUri));
            }

            _authorizationUri = authorizationUri;

            InitializeComponent();
        }

        public string AuthorizationCode
        {
            get;
            private set;
        }

        public bool IsHttpError { get; private set; }

        private void AuthorizationDialog_Shown(object sender, EventArgs e)
        {
            ((SHDocVw.WebBrowser)webBrowser.ActiveXInstance).NavigateError += WebBrowser_NavigateError;

            webBrowser.Navigate(_authorizationUri);
        }

        private bool CheckUrlQuery(Uri url)
        {
            var queryString = url.Query;
            var lastIndex = queryString.LastIndexOf('?');
            if (lastIndex < 0)
            {
                return false;
            }

            queryString = queryString.Substring(lastIndex);
            var query = HttpUtility.ParseQueryString(queryString);
            var code = query.Get("code");
            if (code == null)
            {
                return false;
            }

            AuthorizationCode = code;
            DialogResult = DialogResult.OK;
            return true;
        }

        private void WebBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            CheckUrlQuery(e.Url);
        }

        private void WebBrowser_NavigateError(object pDisp, ref object URL, ref object Frame, ref object StatusCode, ref bool Cancel)
        {
            if (CheckUrlQuery(new Uri((string)URL)))
            {
                return;
            }

            IsHttpError = true;
            DialogResult = DialogResult.Abort;
        }
    }
}
