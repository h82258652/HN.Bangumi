using System;
using System.Web;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace HN.Bangumi.API.Authorization
{
    public partial class AuthorizationDialog : Form
    {
        public AuthorizationDialog(Uri authorizationUri)
        {
            if (authorizationUri == null)
            {
                throw new ArgumentNullException(nameof(authorizationUri));
            }

            InitializeComponent();
            var webBrowser = new ChromiumWebBrowser(authorizationUri.ToString());
            webBrowser.AddressChanged += WebBrowser_AddressChanged;
            webBrowser.LoadError += WebBrowser_LoadError;
            panel.Controls.Add(webBrowser);
        }

        public string AuthorizationCode
        {
            get;
            private set;
        }

        public bool IsHttpError { get; private set; }
        
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

        private void WebBrowser_AddressChanged(object sender, CefSharp.AddressChangedEventArgs e)
        {
            CheckUrlQuery(new Uri(e.Address));
        }

        private void WebBrowser_LoadError(object sender, CefSharp.LoadErrorEventArgs e)
        {
            if (CheckUrlQuery(new Uri(e.FailedUrl)))
            {
                return;
            }

            IsHttpError = true;
            DialogResult = DialogResult.Abort;
        }
    }
}
