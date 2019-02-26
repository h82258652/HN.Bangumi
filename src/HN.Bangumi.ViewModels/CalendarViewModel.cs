using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using HN.Bangumi.API.Models;
using HN.Bangumi.Services;

namespace HN.Bangumi.ViewModels
{
    public class CalendarViewModel : ViewModelBase
    {
        private readonly ICalendarService _calendarService;
        private readonly INavigationService _navigationService;
        private Calendar[] _calendar;
        private bool _isLoading;
        private RelayCommand<Subject> _itemClickCommand;

        public CalendarViewModel(ICalendarService calendarService, INavigationService navigationService)
        {
            _calendarService = calendarService;
            _navigationService = navigationService;

            Load();
        }

        public Calendar[] Calendar
        {
            get => _calendar;
            private set => Set(ref _calendar, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set => Set(ref _isLoading, value);
        }

        public RelayCommand<Subject> ItemClickCommand
        {
            get
            {
                _itemClickCommand = _itemClickCommand ?? new RelayCommand<Subject>(subject =>
                {
                    _navigationService.NavigateTo(ViewKeys.SubjectViewKey, subject.Id);
                });
                return _itemClickCommand;
            }
        }

        private async void Load()
        {
            try
            {
                IsLoading = true;

                Calendar = await _calendarService.GetAsync();
            }
            catch (Exception ex)
            {
                // TODO
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
