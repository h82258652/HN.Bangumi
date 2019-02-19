using Windows.UI.Xaml.Controls;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingView));
            }
            else
            {
                var invokedItem = args.InvokedItem;
            }
        }
    }
}
