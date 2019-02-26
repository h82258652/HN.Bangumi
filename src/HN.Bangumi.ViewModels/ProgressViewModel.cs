using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HN.Bangumi.Services;

namespace HN.Bangumi.ViewModels
{
    public class ProgressViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private bool _isLoading;
        private RelayCommand _refreshCommand;

        public ProgressViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set => Set(ref _isLoading, value);
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                _refreshCommand = _refreshCommand ?? new RelayCommand(() =>
                {
                    // TODO
                });
                return _refreshCommand;
            }
        }

        private async void Load()
        {
            try
            {
                IsLoading = true;

            }
            catch (Exception ex) { }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
