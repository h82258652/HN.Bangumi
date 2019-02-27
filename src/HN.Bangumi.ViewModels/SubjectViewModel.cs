using System;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using HN.Bangumi.API.Models;
using HN.Bangumi.Services;

namespace HN.Bangumi.ViewModels
{
    public class SubjectViewModel : ViewModelBase
    {
        private readonly ISubjectService _subjectService;
        private bool _isLoading;
        private Subject _subject;

        public SubjectViewModel(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set => Set(ref _isLoading, value);
        }

        public Subject Subject
        {
            get => _subject;
            private set => Set(ref _subject, value);
        }

        public async void Load(int id)
        {
            try
            {
                IsLoading = true;

                Subject = await _subjectService.GetAsync(id);
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
