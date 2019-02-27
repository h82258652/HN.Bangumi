using System.Diagnostics;
using HN.Bangumi.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.Extensions;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class SearchView
    {
        private ScrollViewer _animeScrollViewer;
        private ScrollViewer _bookScrollViewer;
        private ScrollViewer _gameScrollViewer;
        private ScrollViewer _musicScrollViewer;
        private ScrollViewer _realScrollViewer;

        public SearchView()
        {
            InitializeComponent();
        }

        public SearchViewModel ViewModel => (SearchViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var query = e.Parameter as string;
            if (query != null)
            {
                AutoSuggestBox.Text = query;
                ViewModel.XCommand.Execute(query);
            }
        }

        private void AnimeGridView_Loaded(object sender, RoutedEventArgs e)
        {
            _animeScrollViewer = AnimeGridView.GetScrollViewer();
            _animeScrollViewer.ViewChanged += AnimeScrollViewer_ViewChanged;
        }

        private void AnimeScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            Debug.WriteLine(_animeScrollViewer.ScrollableHeight);
            Debug.WriteLine(_animeScrollViewer.ViewportHeight);
            Debug.WriteLine(_animeScrollViewer.ExtentHeight);
            Debug.WriteLine(_animeScrollViewer.VerticalOffset);
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ViewModel.XCommand.Execute(args.QueryText);
        }

        private void BookGridView_Loaded(object sender, RoutedEventArgs e)
        {
            _bookScrollViewer = BookGridView.GetScrollViewer();
            _bookScrollViewer.ViewChanged += BookScrollViewer_ViewChanged;
        }

        private void BookScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            // TODO
        }

        private void GameGridView_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void MusicGridView_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void RealGridView_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
