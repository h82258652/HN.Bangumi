using System;
using System.Net.Http;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HN.Bangumi.API;
using HN.Bangumi.API.Authorization;
using HN.Bangumi.API.Models;
using HN.Bangumi.Services;

namespace HN.Bangumi.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly IAppDialogService _appDialogService;
        private readonly IBangumiClient _client;
        private readonly IUserService _userService;
        private bool _isBusy;
        private RelayCommand _signInCommand;
        private RelayCommand _signOutCommand;
        private User _user;

        public ShellViewModel(
            IBangumiClient client, 
            IUserService userService, 
            IAppDialogService appDialogService,
            IAppToastService appToastService)
        {
            _client = client;
            _userService = userService;
            _appDialogService = appDialogService;
            _appToastService = appToastService;

            if (IsSignIn)
            {
                LoadUser();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            private set => Set(ref _isBusy, value);
        }

        public bool IsSignIn => _client.IsSignIn;

        public RelayCommand SignInCommand
        {
            get
            {
                _signInCommand = _signInCommand ?? new RelayCommand(async () =>
                {
                    try
                    {
                        IsBusy = true;

                        await _client.SignInAsync();

                        LoadUser();
                    }
                    catch (UserCancelAuthorizationException)
                    {
                    }
                    catch (Exception ex) when (ex is HttpErrorAuthorizationException || ex is HttpRequestException)
                    {
                        _appToastService.ShowError("网络错误，请稍后重试");
                    }
                    finally
                    {
                        RaisePropertyChanged(nameof(IsSignIn));

                        IsBusy = false;
                    }
                });
                return _signInCommand;
            }
        }

        private readonly IAppToastService _appToastService;

        public RelayCommand SignOutCommand
        {
            get
            {
                _signOutCommand = _signOutCommand ?? new RelayCommand(async () =>
                {
                    var result = await _appDialogService.ShowConfirmAsync("确认登出吗？", string.Empty, "登出", "取消");
                    if (!result)
                    {
                        return;
                    }

                    try
                    {
                        IsBusy = true;

                        await _client.SignOutAsync();
                    }
                    finally
                    {
                        RaisePropertyChanged(nameof(IsSignIn));

                        IsBusy = false;
                    }
                });
                return _signOutCommand;
            }
        }

        public User User
        {
            get => _user;
            private set => Set(ref _user, value);
        }

        private async void LoadUser()
        {
            try
            {
                if (!IsSignIn)
                {
                    return;
                }

                var result = await _userService.GetUserAsync(_client.UserId);
                if (result.ErrorCode == 0)
                {
                    User = result;
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
