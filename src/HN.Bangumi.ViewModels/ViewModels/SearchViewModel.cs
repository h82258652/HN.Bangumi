using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using HN.Bangumi.API.Models;
using HN.Bangumi.Services;

namespace HN.Bangumi.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private const int LoadCount = 20;
        private readonly IAppToastService _appToastService;
        private readonly INavigationService _navigationService;
        private readonly ISubjectService _subjectService;
        private bool _hasMoreAnimeItems;
        private bool _hasMoreBookItems;
        private bool _hasMoreGameItems;
        private bool _hasMoreMusicItems;
        private bool _hasMoreRealItems;
        private bool _isLoadingAnimes;
        private bool _isLoadingBooks;
        private bool _isLoadingGames;
        private bool _isLoadingMusics;
        private bool _isLoadingReals;
        private RelayCommand<Subject> _itemClickCommand;
        private string _lastQuery;
        private CancellationTokenSource _lastQueryCts;
        private RelayCommand _loadMoreAnimesCommand;
        private RelayCommand _loadMoreBooksCommand;
        private RelayCommand _loadMoreGamesCommand;
        private RelayCommand _loadMoreMusicsCommand;
        private RelayCommand _loadMoreRealsCommand;
        private RelayCommand<string> _searchCommand;

        public SearchViewModel(
            INavigationService navigationService,
            ISubjectService subjectService,
            IAppToastService appToastService)
        {
            _navigationService = navigationService;
            _subjectService = subjectService;
            _appToastService = appToastService;
        }

        public ObservableCollection<Subject> Animes { get; } = new ObservableCollection<Subject>();

        public ObservableCollection<Subject> Books { get; } = new ObservableCollection<Subject>();

        public ObservableCollection<Subject> Games { get; } = new ObservableCollection<Subject>();

        public bool HasMoreAnimeItems
        {
            get => _hasMoreAnimeItems;
            private set => Set(ref _hasMoreAnimeItems, value);
        }

        public bool HasMoreBookItems
        {
            get => _hasMoreBookItems;
            private set => Set(ref _hasMoreBookItems, value);
        }

        public bool HasMoreGameItems
        {
            get => _hasMoreGameItems;
            private set => Set(ref _hasMoreGameItems, value);
        }

        public bool HasMoreMusicItems
        {
            get => _hasMoreMusicItems;
            private set => Set(ref _hasMoreMusicItems, value);
        }

        public bool HasMoreRealItems
        {
            get => _hasMoreRealItems;
            private set => Set(ref _hasMoreRealItems, value);
        }

        public bool IsLoadingAnimes
        {
            get => _isLoadingAnimes;
            private set => Set(ref _isLoadingAnimes, value);
        }

        public bool IsLoadingBooks
        {
            get => _isLoadingBooks;
            private set => Set(ref _isLoadingBooks, value);
        }

        public bool IsLoadingGames
        {
            get => _isLoadingGames;
            private set => Set(ref _isLoadingGames, value);
        }

        public bool IsLoadingMusics
        {
            get => _isLoadingMusics;
            private set => Set(ref _isLoadingMusics, value);
        }

        public bool IsLoadingReals
        {
            get => _isLoadingReals;
            private set => Set(ref _isLoadingReals, value);
        }

        public RelayCommand<Subject> ItemClickCommand
        {
            get
            {
                _itemClickCommand = _itemClickCommand ?? new RelayCommand<Subject>(item =>
                {
                    _navigationService.NavigateTo(ViewKeys.SubjectViewKey, item.Id);
                });
                return _itemClickCommand;
            }
        }

        public RelayCommand LoadMoreAnimesCommand
        {
            get
            {
                _loadMoreAnimesCommand = _loadMoreAnimesCommand ?? new RelayCommand(async () =>
                {
                    if (IsLoadingAnimes || !HasMoreAnimeItems)
                    {
                        return;
                    }

                    try
                    {
                        IsLoadingAnimes = true;

                        var result = await _subjectService.SearchAnimeAsync(_lastQuery, Animes.Count, LoadCount, _lastQueryCts.Token);
                        if (result.ErrorCode == 0)
                        {
                            if (result.List != null)
                            {
                                foreach (var item in result.List)
                                {
                                    if (Animes.All(temp => temp.Id != item.Id))
                                    {
                                        Animes.Add(item);
                                    }
                                }
                            }

                            if (result.List == null || result.List.Length == 0)
                            {
                                HasMoreAnimeItems = false;
                            }
                        }
                        else
                        {
                            _appToastService.ShowError(result.ErrorMessage);
                        }
                    }
                    catch (HttpRequestException)
                    {
                        _appToastService.ShowError("搜索动画失败，请稍后重试");
                    }
                    finally
                    {
                        IsLoadingAnimes = false;
                    }
                });
                return _loadMoreAnimesCommand;
            }
        }

        public RelayCommand LoadMoreBooksCommand
        {
            get
            {
                _loadMoreBooksCommand = _loadMoreBooksCommand ?? new RelayCommand(async () =>
                {
                    if (IsLoadingBooks || !HasMoreBookItems)
                    {
                        return;
                    }

                    try
                    {
                        IsLoadingBooks = true;

                        var result = await _subjectService.SearchBookAsync(_lastQuery, Books.Count, LoadCount, _lastQueryCts.Token);
                        if (result.ErrorCode == 0)
                        {
                            if (result.List != null)
                            {
                                foreach (var item in result.List)
                                {
                                    if (Books.All(temp => temp.Id != item.Id))
                                    {
                                        Books.Add(item);
                                    }
                                }
                            }

                            if (result.List == null || result.List.Length == 0)
                            {
                                HasMoreBookItems = false;
                            }
                        }
                        else
                        {
                            _appToastService.ShowError(result.ErrorMessage);
                        }
                    }
                    catch (HttpRequestException)
                    {
                        _appToastService.ShowError("搜索书籍失败，请稍后重试");
                    }
                    finally
                    {
                        IsLoadingBooks = false;
                    }
                });
                return _loadMoreBooksCommand;
            }
        }

        public RelayCommand LoadMoreGamesCommand
        {
            get
            {
                _loadMoreGamesCommand = _loadMoreGamesCommand ?? new RelayCommand(async () =>
                {
                    if (IsLoadingGames || !HasMoreGameItems)
                    {
                        return;
                    }

                    try
                    {
                        IsLoadingGames = true;

                        var result = await _subjectService.SearchGameAsync(_lastQuery, Games.Count, LoadCount, _lastQueryCts.Token);
                        if (result.ErrorCode == 0)
                        {
                            if (result.List != null)
                            {
                                foreach (var item in result.List)
                                {
                                    if (Games.All(temp => temp.Id != item.Id))
                                    {
                                        Games.Add(item);
                                    }
                                }
                            }

                            if (result.List == null || result.List.Length == 0)
                            {
                                HasMoreGameItems = true;
                            }
                        }
                        else
                        {
                            _appToastService.ShowError(result.ErrorMessage);
                        }
                    }
                    catch (HttpRequestException)
                    {
                        _appToastService.ShowError("搜索游戏失败，请稍后重试");
                    }
                    finally
                    {
                        IsLoadingGames = false;
                    }
                });
                return _loadMoreGamesCommand;
            }
        }

        public RelayCommand LoadMoreMusicsCommand
        {
            get
            {
                _loadMoreMusicsCommand = _loadMoreMusicsCommand ?? new RelayCommand(async () =>
                {
                    if (IsLoadingMusics || !HasMoreMusicItems)
                    {
                        return;
                    }

                    try
                    {
                        IsLoadingMusics = true;

                        var result = await _subjectService.SearchMusicAsync(_lastQuery, Musics.Count, LoadCount, _lastQueryCts.Token);
                        if (result.ErrorCode == 0)
                        {
                            if (result.List != null)
                            {
                                foreach (var item in result.List)
                                {
                                    if (Musics.All(temp => temp.Id != item.Id))
                                    {
                                        Musics.Add(item);
                                    }
                                }
                            }

                            if (result.List == null || result.List.Length == 0)
                            {
                                HasMoreMusicItems = false;
                            }
                        }
                        else
                        {
                            _appToastService.ShowError(result.ErrorMessage);
                        }
                    }
                    catch (HttpRequestException)
                    {
                        _appToastService.ShowError("搜索音乐失败，请稍后重试");
                    }
                    finally
                    {
                        IsLoadingMusics = false;
                    }
                });
                return _loadMoreMusicsCommand;
            }
        }

        public RelayCommand LoadMoreRealsCommand
        {
            get
            {
                _loadMoreRealsCommand = _loadMoreRealsCommand ?? new RelayCommand(async () =>
                {
                    if (IsLoadingReals || !HasMoreRealItems)
                    {
                        return;
                    }

                    try
                    {
                        IsLoadingReals = true;

                        var result = await _subjectService.SearchRealAsync(_lastQuery, Reals.Count, LoadCount, _lastQueryCts.Token);
                        if (result.ErrorCode == 0)
                        {
                            if (result.List != null)
                            {
                                foreach (var item in result.List)
                                {
                                    if (Reals.All(temp => temp.Id != item.Id))
                                    {
                                        Reals.Add(item);
                                    }
                                }
                            }

                            if (result.List == null || result.List.Length == 0)
                            {
                                HasMoreRealItems = false;
                            }
                        }
                        else
                        {
                            _appToastService.ShowError(result.ErrorMessage);
                        }
                    }
                    catch (HttpRequestException)
                    {
                        _appToastService.ShowError("搜索三次元失败，请稍后重试");
                    }
                    finally
                    {
                        IsLoadingReals = false;
                    }
                });
                return _loadMoreRealsCommand;
            }
        }

        public ObservableCollection<Subject> Musics { get; } = new ObservableCollection<Subject>();

        public ObservableCollection<Subject> Reals { get; } = new ObservableCollection<Subject>();

        public RelayCommand<string> SearchCommand
        {
            get
            {
                _searchCommand = _searchCommand ?? new RelayCommand<string>(query =>
                {
                    if (query == _lastQuery)
                    {
                        return;
                    }

                    _lastQueryCts?.Cancel();

                    Animes.Clear();
                    Books.Clear();
                    Musics.Clear();
                    Games.Clear();
                    Reals.Clear();

                    _lastQuery = query;
                    _lastQueryCts = new CancellationTokenSource();

                    HasMoreAnimeItems = true;
                    HasMoreBookItems = true;
                    HasMoreMusicItems = true;
                    HasMoreGameItems = true;
                    HasMoreRealItems = true;

                    LoadMoreAnimesCommand.Execute(null);
                    LoadMoreBooksCommand.Execute(null);
                    LoadMoreMusicsCommand.Execute(null);
                    LoadMoreGamesCommand.Execute(null);
                    LoadMoreRealsCommand.Execute(null);
                });
                return _searchCommand;
            }
        }
    }
}
