using HN.Bangumi.ViewModels;
using Windows.UI.Xaml.Controls;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        public ShellViewModel ViewModel => (ShellViewModel)DataContext;

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var query = args.QueryText;
            ContentFrame.Navigate(typeof(SearchView), query);
        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingView));
            }
            else
            {
                var itemContainer = args.InvokedItemContainer;
                switch (itemContainer.Tag?.ToString())
                {
                    case "Progress":
                        ContentFrame.Navigate(typeof(ProgressView));
                        break;

                    case "Calendar":
                        ContentFrame.Navigate(typeof(CalendarView));
                        break;
                }
            }
        }
    }
}
