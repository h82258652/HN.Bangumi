using System.Net.Http;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using HN.Bangumi.API;
using HN.Bangumi.API.Models;
using HN.Bangumi.Messages;
using HN.Bangumi.Services;

namespace HN.Bangumi.ViewModels
{
    public class ProgressViewModel : ViewModelBase
    {
        private readonly IAppToastService _appToastService;
        private readonly IBangumiClient _bangumiClient;
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private CollectionSubject[] _collection;
        private bool _isLoading;
        private RelayCommand<CollectionSubject> _itemClickCommand;
        private RelayCommand _refreshCommand;

        public ProgressViewModel(
            IUserService userService,
            IBangumiClient bangumiClient,
            INavigationService navigationService,
            IAppToastService appToastService)
        {
            _userService = userService;
            _bangumiClient = bangumiClient;
            _navigationService = navigationService;
            _appToastService = appToastService;

            MessengerInstance.Register<SignedInMessage>(this, message =>
            {
                RefreshCommand.Execute(null);
            });
            MessengerInstance.Register<SignedOutMessage>(this, message =>
            {
                Collection = null;
            });

            if (_bangumiClient.IsSignIn)
            {
                RefreshCommand.Execute(null);
            }
        }

        public CollectionSubject[] Collection
        {
            get => _collection;
            private set => Set(ref _collection, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set => Set(ref _isLoading, value);
        }

        public RelayCommand<CollectionSubject> ItemClickCommand
        {
            get
            {
                _itemClickCommand = _itemClickCommand ?? new RelayCommand<CollectionSubject>(collectionSubject =>
                {
                    _navigationService.NavigateTo(ViewKeys.SubjectViewKey, collectionSubject.SubjectId);
                });
                return _itemClickCommand;
            }
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                _refreshCommand = _refreshCommand ?? new RelayCommand(async () =>
                {
                    if (IsLoading || !_bangumiClient.IsSignIn)
                    {
                        return;
                    }

                    try
                    {
                        IsLoading = true;

                        var userId = _bangumiClient.UserId;
                        Collection = await _userService.GetCollectionAsync(userId);
                    }
                    catch (HttpRequestException)
                    {
                        _appToastService.ShowError("加载失败，请稍后重试");
                    }
                    finally
                    {
                        IsLoading = false;
                    }
                });
                return _refreshCommand;
            }
        }
    }
}
