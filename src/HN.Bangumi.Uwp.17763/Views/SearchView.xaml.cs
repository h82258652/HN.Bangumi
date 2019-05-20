using HN.Bangumi.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using HN.Bangumi.API.Models;
using HN.Bangumi.Uwp.Extensions;
using WinRTXamlToolkit.Controls.Extensions;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class SearchView
    {
        private const double IncrementalLoadingThreshold = 300;
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
                ViewModel.SearchCommand.Execute(query);
            }
        }

        private void AnimeGridView_Loaded(object sender, RoutedEventArgs e)
        {
            _animeScrollViewer = AnimeGridView.GetScrollViewer();
            _animeScrollViewer.ViewChanged += AnimeScrollViewer_ViewChanged;
        }

        private void AnimeScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (_animeScrollViewer.VerticalOffset >= _animeScrollViewer.ScrollableHeight - IncrementalLoadingThreshold)
            {
                ViewModel.LoadMoreAnimesCommand.Execute(null);
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ViewModel.SearchCommand.Execute(args.QueryText);
        }

        private void BookGridView_Loaded(object sender, RoutedEventArgs e)
        {
            _bookScrollViewer = BookGridView.GetScrollViewer();
            _bookScrollViewer.ViewChanged += BookScrollViewer_ViewChanged;
        }

        private void BookScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (_bookScrollViewer.VerticalOffset >= _bookScrollViewer.ScrollableHeight - IncrementalLoadingThreshold)
            {
                ViewModel.LoadMoreBooksCommand.Execute(null);
            }
        }

        private void GameGridView_Loaded(object sender, RoutedEventArgs e)
        {
            _gameScrollViewer = GameGridView.GetScrollViewer();
            _gameScrollViewer.ViewChanged += GameScrollViewer_ViewChanged;
        }

        private void GameScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (_gameScrollViewer.VerticalOffset >= _gameScrollViewer.ScrollableHeight - IncrementalLoadingThreshold)
            {
                ViewModel.LoadMoreGamesCommand.Execute(null);
            }
        }

        private void MusicGridView_Loaded(object sender, RoutedEventArgs e)
        {
            _musicScrollViewer = MusicGridView.GetScrollViewer();
            _musicScrollViewer.ViewChanged += MusicScrollViewer_ViewChanged;
        }

        private void MusicScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (_musicScrollViewer.VerticalOffset >= _musicScrollViewer.ScrollableHeight - IncrementalLoadingThreshold)
            {
                ViewModel.LoadMoreMusicsCommand.Execute(null);
            }
        }

        private void RealGridView_Loaded(object sender, RoutedEventArgs e)
        {
            _realScrollViewer = RealGridView.GetScrollViewer();
            _realScrollViewer.ViewChanged += RealScrollViewer_ViewChanged;
        }

        private void RealScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (_realScrollViewer.VerticalOffset >= _realScrollViewer.ScrollableHeight - IncrementalLoadingThreshold)
            {
                ViewModel.LoadMoreRealsCommand.Execute(null);
            }
        }

        private void SubjectGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemsView = (ListViewBase)sender;
            var subject = (Subject)e.ClickedItem;
            var animation = itemsView.PrepareConnectedAnimation("SubjectForwardAnimation", subject, "SubjectImage");
            animation.Configuration = new BasicConnectedAnimationConfiguration();
            animation.SetExtraData("SubjectImageUrl", subject.Images.Common);
        }
    }
}
